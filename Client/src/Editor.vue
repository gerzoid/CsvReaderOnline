<script setup>
import CsvEditor from "./components/CsvEditor.vue";
import { showNotification } from "./plugins/notification";
import { LayoutContent, LayoutFooter, Layout } from "ant-design-vue";
import { useFileStore } from "./stores/filestore";
import FileMenu from "./components/Menu.vue";
import Api from "./plugins/api";

const fileStore = useFileStore();
fileStore.fileInfo = JSON.parse(localStorage.getItem('csveditor_fileinfo'));
</script>

<template>
  <layout class="layout">
    <layout-content id="content">
      <file-menu></file-menu>
      <div class="subcontent">
        <csv-editor></csv-editor>
      </div>
    </layout-content>
    <layout-footer style="text-align: center"> jobtools.ru ©2023 </layout-footer>
  </layout>

</template>

<style scoped>
</style>
