<script setup>
import UploadFile from './components/UploadFile.vue'
import ListUploadFiles from './components/ListUploadFiles.vue'
import Api from "./plugins/api";
import { useFileStore } from "./stores/filestore";
import { ref, toRaw } from "vue";

const fileStore = useFileStore();
var listUploadedFiles = ref(null);

var onSelectedFile = (file) => {
  var fileName=file.filesId + "."+file.originalName.split(".").pop();
  Api.OpenFile(fileName)
    .then((result) => {
      localStorage.setItem('csveditor_fileinfo', JSON.stringify(result.data.info));
      localStorage.setItem('csveditor_settings', JSON.stringify(result.data.settings));
      window.location.replace('/editor');
    })
    .catch((e) => {
      console.log(e);
    });
};

</script>

<template>
  <UploadFile></UploadFile>
  <list-upload-files @selectedFile="onSelectedFile"></list-upload-files>


</template>

<style scoped>

</style>
