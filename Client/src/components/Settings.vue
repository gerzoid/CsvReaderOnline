<script setup>
import { ref, watch } from "vue";
import { useFileStore } from "../stores/filestore";
import Api from "../plugins/api";
import { SettingOutlined } from "@ant-design/icons-vue";

const fileStore = useFileStore();
var encoding = ref("qwerqwrwqerwer");
var quote = ref(" ");
var activeKey = ref(["1"]);
</script>

<template>
  <a-collapse v-model:activeKey="activeKey">
    <a-collapse-panel key="1" header="Настройки чтения CSV файла">
      <template #extra><setting-outlined /></template>
      <div class="csv-settings">
        <a-input-group size="small">
          <a-row :gutter="8">
            <a-col :span="5">
              <a-form-item label="Кодировка">
                <a-select
                  v-model:value="fileStore.settings.encoding"
                  size="small"
                  style="width: 200px"
                >
                  <a-select-option value="utf-8">UTF-8</a-select-option>
                  <a-select-option value="Windows-1251">CP1251</a-select-option>
                  <a-select-option value="koi8r">KOI8-R</a-select-option>
                </a-select>
              </a-form-item>
            </a-col>
            <a-col :span="8">
              <a-form-item label="Разделитель">
                <a-input
                  v-model:value="fileStore.settings.separator"
                  size="small"
                  placeholder="Авто"
                  style="width: 80px"
                />
              </a-form-item>
            </a-col>
            <a-switch v-model:checked="fileStore.settings.hasHeader" />
          </a-row>
        </a-input-group>
      </div>
    </a-collapse-panel>
  </a-collapse>
</template>
