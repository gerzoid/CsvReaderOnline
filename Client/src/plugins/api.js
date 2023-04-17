import {axio} from './axios';
import { getCookie, setCookie } from "./cookies";
import { useFileStore } from "../stores/filestore";

export default class Api {

    //Проверитьь список загруженных файлов по юзеру
    static CheckUploadedFiles() {
        const data = new FormData();
        data.append("userId", getCookie("dbfshowuser"));
        return axio
          .post("/api/users/check", data, { proxy: false })
    }

    //Открыть файл
    static OpenFile(fileName){
        var formData = new FormData();
        formData.append("fileName", fileName);
        return axio.post("/api/Files/open", formData);
    }

    //Получить данные
    static GetData(){
        const fileStore = useFileStore();
        var data={
            "FileName" :fileStore.fileInfo.fileName,
            "Settings":fileStore.settings,
            "Options":fileStore.options,
            "CountColumns": fileStore.fileInfo.countColumns,
            "CountRows": fileStore.fileInfo.countRecords,
            "FileInfo": fileStore.fileInfo
        }
        //Признак необходимости сохранить изменения настроек файла в БД
        data.Options = {'needSaveSettings':fileStore.needSaveSettings, ...data.Options  };
        return axio.post("/api/editor/getData", data);
    }

    //Загрузка файла
    static UploadFile(file){
        const fileStore = useFileStore();
        var formData = new FormData();
        formData.append("formfile", file);
        formData.append("filename", file.name);
        formData.append("userId", fileStore.userId);
        return axio
        .post("/api/Files", formData, {
          headers: {
            "Content-Type": "multipart/form-data",
          },
        });
    }

}