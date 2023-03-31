<script setup>
import { ref, watch } from "vue";
import { useFileStore } from "../stores/filestore";
import Api from "../plugins/api";
import { Menu, SubMenu, MenuItem,LayoutHeader } from "ant-design-vue";

const fileStore = useFileStore();
var selectedKeys = ref([]);

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
  <layout-header>
    <div class="logo" />

    <menu
      class="main-menu"      
      theme="dark"
      mode="horizontal"
      @click="onClick"
    >
      <sub-menu key="1">
        <template selected #title>Файл</template>
        <menu-item :disabled="!fileStore.itsLoaded" key="close">Закрыть</menu-item>
        <menu-item disabled key="export">Экспорт</menu-item>
        <menu-item
          :disabled="!fileStore.itsLoaded"
          @click="Api.DownloadFile()"
          key="save"
        >
          Скачать
        </menu-item>
      </sub-menu>
      <menu-item disabled key="2">Правка</menu-item>
      <sub-menu key="3">
        <template #title>Разное</template>
        <menu-item :disabled="!fileStore.itsLoaded" key="codepage"
          >Кодировка</menu-item
        >
      </sub-menu>
      <sub-menu key="4">
        <template #title>Статистика</template>
        <menu-item :disabled="!fileStore.itsLoaded" key="statistics"
          >Статистика...</menu-item
        >
      </sub-menu>
      <sub-menu key="5">
        <template #title>Помощь</template>
        <menu-item key="about">О сервисе</menu-item>
        <menu-item key="message">Сообщение автору</menu-item>
      </sub-menu>
    </menu>
  </layout-header>
</template>
