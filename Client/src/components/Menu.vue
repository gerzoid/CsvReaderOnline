<script setup>
import { ref, watch } from "vue";
import { useFileStore } from "../stores/filestore";
import Api from "../plugins/api";

const fileStore = useFileStore();
var selectedKeys = ref([]);

function test(){
  fileStore.visibleSettings = true;
}

function onClick(e) {
  switch (e.key) {
    case "close":
      //fileStore.closeFile();
      break;
    case "about":
      //fileStore.activeModalComponent = "About";
      break;
    case "codepage":
      //fileStore.activeModalComponent = "Codepage";
      break;
    case "statistics":
      //fileStore.activeModalComponent = "Statistics";
      break;
    case "message":
      //fileStore.activeModalComponent = "MessageAuthor";
      break;
  }
}
</script>

<template>
  <a-layout-header>
    <div class="logo" />

    <a-menu
      v-model:selectedKeys="selectedKeys"
      class="main-menu" 
      theme="dark"
      mode="horizontal"
      @click="onClick"
    >
      <a-sub-menu key="1">
        <template selected #title>Файл</template>
        <a-menu-item :disabled="!fileStore.itsLoaded" key="close">Закрыть</a-menu-item>
        <a-menu-item disabled key="export">Экспорт</a-menu-item>
        <a-menu-item
          :disabled="!fileStore.itsLoaded"
          @click="Api.DownloadFile()"
          key="save"
        >
          Скачать
        </a-menu-item>
      </a-sub-menu>
      <a-menu-item key="2" @click="test">Настройки</a-menu-item>
      <a-sub-menu key="5">
        <template #title>Помощь</template>
        <a-menu-item key="about">О сервисе</a-menu-item>
        <a-menu-item key="message">Сообщение автору</a-menu-item>
      </a-sub-menu>
    </a-menu>
  </a-layout-header>
</template>
