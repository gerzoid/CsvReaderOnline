import { createApp } from 'vue'
import { createPinia } from 'pinia'
import Editor from './Editor.vue'

let Pinia = createPinia();

//createApp(AppMain).use(Antd).use(Pinia).mount('#app')
createApp(Editor).use(Pinia).mount('#app')
