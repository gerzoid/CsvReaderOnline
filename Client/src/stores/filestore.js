import { treeNodeProps } from 'ant-design-vue/lib/vc-tree/props';
import { defineStore } from 'pinia'
import Api from "../plugins/api";
import { showNotification } from "../plugins/notification";
import { setCookie } from "../plugins/cookies";

export const useFileStore = defineStore('fileStore', {
    state: () => ({
      fileInfo: [{
        fileName:"",
        fileId:"",
        countRecords:0,
        countColumns:0,
        columns:null}
      ],
      selectedColumnType:'',
      options: {
        page:1,
        pageSize:50,
      },
      settings: {
        separator:"",
        quote: '|',
        hasHeader:true,
        encoding:'Windows-1251',
      },
      fileName: 'ff2b5992-d722-413a-8bb8-edd17cdae392.csv',
      originalFileName:'',
      needSaveSettings:false,
      itsLoaded: false,
      listUploadedFiles: null,
      needReload: false,
      activeModalComponent: null,
      visibleSettings: true, //панель настроек a-drawer bottom
    }),
    getters: {
    },
    actions: {
      UploadFile(file){
        var self = this;
        return new Promise(function(resolve, reject){
          var hasError = false;
          self.originalFileName = file.name;
          Api.UploadFile(file)
            .then((data) => {
              self.itsLoaded= true
              self.fileInfo = data.data;
              console.log('sd');
              resolve();
            })
            .catch((e) => {
              reject(e);
              console.log("Ошибка: " + e);
            });
          });
      },
      CheckUploadedFiles(){
        var self = this;
        return new Promise(function(resolve, reject){
          Api.CheckUploadedFiles()
            .then((result) => {
              let date = new Date();
              date = new Date(date.setMonth(date.getMonth() + 8));
              setCookie("dbfshowuser", result.data.usersId, { expiries: date.toUTCString() });
              self.userId = result.data.usersId;
              console.log(result);
              resolve(result.data.files);
            })
            .catch((e) => {
              console.log(e);
              reject(e);
            });
        });
      },
      GetData(){
        var self = this;
        return new Promise(function(resolve, reject){
          Api.GetData()
            .then((result) => {
              resolve(result);
            })
            .catch((e) => {
              console.log(e);
              reject(e);
            });
          });
      },
      //Закрываем открытый файл
      closeFile(){
        this.itsLoaded = false;
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