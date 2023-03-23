import { createApp } from 'vue'
import './style.css'
import { createPinia } from 'pinia'
//import Antd from 'ant-design-vue';
import 'ant-design-vue/dist/antd.css';
import App from './App.vue'

let Pinia = createPinia();

//createApp(AppMain).use(Antd).use(Pinia).mount('#app')
createApp(App).use(Pinia).mount('#app')
