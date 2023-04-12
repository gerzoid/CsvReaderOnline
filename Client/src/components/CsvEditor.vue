<script setup>

import { HotTable } from "@handsontable/vue3";
import { useFileStore } from "../stores/filestore";
import Settings from "../components/Settings.vue";
import { registerAllModules } from "handsontable/registry";
import { ref, watch, toRaw, onMounted } from "vue";
import Spinner from "../components/Spinner.vue";
import "handsontable/dist/handsontable.full.min.css";

const fileStore = useFileStore();

var hot = ref(null);

registerAllModules();

var settings = ref({
  licenseKey: "non-commercial-and-evaluation",
  columns: toRaw(fileStore.fileInfo.columns),
  rowHeaders(index) {
    return (fileStore.options.page - 1) * fileStore.options.pageSize + index + 1;
  },
  colHeaders: true,
  width: '100%',
  height:'100%',
  manualColumnResize: true,
  columnSorting: true,
  wordWrap: false,
  autoColumnSize: true,
});

//Отдельно для настроек, чобы вместе с получением данных еще и сохрантья в БД настройки файла
watch(
()=>fileStore.settings, (newValue, oldValue)=>{
  console.log('settings change')  ;
  fileStore.options.needSaveSettings = true;
    getData();
    fileStore.options.needSaveSettings = false;
  },{deep: true});

//Отдельно для навигации
watch(
  ()=>fileStore.options, (newValue, oldValue)=>{
    getData();
    fileStore.needReload = false;
  },{deep: true});



//Получение данных с сервера
function getData() {
  fileStore.GetData().then(
    result=>{
      fileStore.fileInfo.columns = result.data.columns;
      hot.value.hotInstance.updateSettings({
        columns : fileStore.fileInfo.columns
      });
      hot.value.hotInstance.updateData(result.data.data);},
    error=>{console.log(error);}
  );
}

getData();

</script>

<template>
   <hot-table ref="hot" :settings="settings"></hot-table>
    <settings></settings>
</template>

<style scoped>
.upload-dbf {
  width: 400px;
  display: block;
  margin-left: auto;
  margin-right: auto;
  background-color: white;
  padding-top: 50px;
}
</style>
