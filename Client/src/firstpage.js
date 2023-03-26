import { createApp } from 'vue'
//import './style.css'
import { createPinia } from 'pinia'
//import Antd from 'ant-design-vue';
import AppMain from './AppMain.vue'

let Pinia = createPinia();

//createApp(AppMain).use(Antd).use(Pinia).mount('#app')
createApp(AppMain).use(Pinia).mount('#app')
