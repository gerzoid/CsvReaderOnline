import { defineStore } from 'pinia'

export const useFileStore = defineStore('fileStore', {
    state: () => ({
      fileInfo: [],
      selectedColumnType:'',
      options: {
        page:1,
        pageSize:50,
      },
      fileName: 'test.csv',
      userId:'',
      originalFileName:'',
      isLoading: false,
      listUploadedFiles: null,
      needReload: false,
      activeModalComponent: null,
    }),
    getters: {
    },
    actions: {
      //Закрываем открытый файл
      closeFile(){
        this.isLoading = false;
        this.fileInfo =[];
        this.fileName ='';
        this.originalFileName ='';
      },
      saveFileName(text) {
        // you can directly mutate the state
        this.fileName= text;
        console.log(text);
      },
    },
  })