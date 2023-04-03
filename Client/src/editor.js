import { createApp } from 'vue'
import { createPinia } from 'pinia'
import Editor from './Editor.vue'
import Antd from 'ant-design-vue';

let Pinia = createPinia();

//createApp(AppMain).use(Antd).use(Pinia).mount('#app')
createApp(Editor).use(Pinia).use(Antd).mount('#app')
