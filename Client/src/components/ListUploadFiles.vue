<script setup>
import Api from "../plugins/api";
import { ref, watch, onMounted } from "vue";
import { Spin } from "ant-design-vue";
import { useFileStore } from "../stores/filestore";
import moment from "moment";

const fileStore = useFileStore();

//const props = defineProps({ files: null });
var files = ref(null);
const emit = defineEmits(["selectedFile"]);
var hasLoadedFiles = false;
var spinnerCheckFiles = ref(false);

//Проверитьь список загруженных файлов по юзеру
function CheckUploadedFiles() {
  spinnerCheckFiles.value = true;
  fileStore.CheckUploadedFiles().then(
    result=>{
      files.value = result;
      spinnerCheckFiles.value=false;
      hasLoadedFiles = true;
    }, error=>{console.log('error')});//.then(error=>{});

}

onMounted(() => {
  CheckUploadedFiles();
});

function onClick(file) {
  emit("selectedFile", file);
}
</script>

<template>
  <spin :spinning="spinnerCheckFiles" size="large">
    <div class="uploadedfiles" v-if="hasLoadedFiles == true">
      <div class="zagolovok">
        Ранее загруженные файлы, доступные для дальнейшего редактирования
      </div>
      <div :key="file.fileId" v-for="(file, index) in files" class="row-uploading">
        <span>
          {{ index + 1 }}. Файл <b> {{ file.originalName }} </b> от
          {{ moment(file.createdAt).format("DD.MM.YYYY hh:mm") }}
        </span>
        <span><a @click="onClick(file)">Открыть</a></span>
      </div>
    </div>
    <div v-else class="zagolovok">Загруженные файлы отсутствуют</div>
  </spin>
</template>

<style>
.uploadedfiles {
  min-height: 130px;
}
.row-uploading {
  padding: 0px 0px;
  width: 100%;
}
.row-uploading span {
  padding: 0px 10px;
  display: inline-block;
}
.zagolovok {
  text-align: center;
  margin: 30px 0px 15px 0px;
}
</style>
