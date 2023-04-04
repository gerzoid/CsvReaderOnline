<script setup>
import { ref, watch } from "vue";
import { useFileStore } from "../stores/filestore";
import Api from "../plugins/api";

const fileStore = useFileStore();


const showDrawer = () => {
  fileStore.visibleSettings = true;
};

const onClose = () => {
  fileStore.visibleSettings  = false;
};

</script>

<template>
<a-drawer
    title="Basic Drawer"
    placement="top"
    height="200"
    :closable="false"
    :visible="fileStore.visibleSettings"
    @close="onClose"
  >
    <p>Some contents...</p>
    <p>Some contents...</p>
    <p>Some contents...</p>
  </a-drawer>
</template>
