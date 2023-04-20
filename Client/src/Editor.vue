<script setup>
import CsvEditor from "./components/CsvEditor.vue";
import Pagination from "./components/Pagination.vue";
import { showNotification } from "./plugins/notification";
import Settings from "./components/Settings.vue";
import { useFileStore } from "./stores/filestore";
import FileMenu from "./components/Menu.vue";
import Api from "./plugins/api";

const fileStore = useFileStore();

fileStore.fileInfo = JSON.parse(localStorage.getItem("csveditor_fileinfo"));
fileStore.settings = JSON.parse(localStorage.getItem("csveditor_settings"));
</script>

<template>
  <a-layout class="layout">
    <a-layout-content id="content">
      <settings></settings>
      <file-menu></file-menu>
      <div class="subcontent">
        <csv-editor></csv-editor>
        <pagination></pagination>
      </div>
    </a-layout-content>
    <a-layout-footer style="text-align: center"> jobtools.ru ©2023 </a-layout-footer>
  </a-layout>
</template>

<style scoped></style>
