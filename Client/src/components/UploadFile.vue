<script setup>
import { InboxOutlined } from "@ant-design/icons-vue";
import { showNotification } from "../plugins/notification";
import { UploadDragger } from "ant-design-vue";
import { ref } from "vue";
import { useFileStore } from "../stores/filestore";
import Api from "../plugins/api";

const emit = defineEmits(["upload-completed"]);
var fileList = ref([]);
const fileStore = useFileStore();

var uploadFiles = ({ onSuccess, onError, file }) => {
  var hasError = false;
  fileStore.UploadFile(file).then(
   (result)=>{
      console.log('uploaded' + result);
      localStorage.setItem('csveditor_fileinfo', JSON.stringify(fileStore.fileInfo));
      localStorage.setItem('csveditor_settings', JSON.stringify(fileStore.settings));
      window.location.replace('/editor');
    },
     error=>{
      showNotification("error", "Внимание", "Файл не загружен. Произошла ошибка", 5)
    });
};
</script>

<template>
  <upload-dragger
    v-model:fileList="fileList"
    name="file"
    accept=".csv"
    :multiple="false"
    :customRequest="uploadFiles"
  >
    <p class="ant-upload-drag-icon">
      <inbox-outlined></inbox-outlined>
    </p>
    <p class="ant-upload-text">
      Щелкните мышкой или перетащите файл в эту область для его загрузки
    </p>
    <p class="ant-upload-hint">Максимальный размер файла <b>50Мб</b></p>
    <p class="ant-upload-hint">Поддерживаются только CSV файлы и TXT с разделителями</p>
    <p>..</p>
  </upload-dragger>
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
