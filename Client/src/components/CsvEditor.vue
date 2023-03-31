<script setup>

import { HotTable } from "@handsontable/vue3";
import { useFileStore } from "../stores/filestore";
import "handsontable/dist/handsontable.full.min.css";
import { registerAllModules } from "handsontable/registry";
import { ref, watch, toRaw, onMounted } from "vue";

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

//Получение данных с сервера
function getData() {
  fileStore.GetData().then(
    result=>{hot.value.hotInstance.updateData(result.data);},
    error=>{console.log(error);}
  );
}

getData();
</script>

<template>
   <hot-table ref="hot" :settings="settings"></hot-table>
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
