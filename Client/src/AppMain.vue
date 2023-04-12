<script setup>
import UploadFile from './components/UploadFile.vue'
import ListUploadFiles from './components/ListUploadFiles.vue'
import Api from "./plugins/api";
import { useFileStore } from "./stores/filestore";
import { ref, watch, onMounted } from "vue";

const fileStore = useFileStore();
var listUploadedFiles = ref(null);

var onSelectedFile = (id, originalName) => {
  Api.OpenFile(id)
    .then((result) => {
      fileStore.fileInfo = result.data;
      fileStore.fileName = result.data.name;
      fileStore.originalFileName = result.data.originalFileName;
      fileStore.isLoading = true;
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
