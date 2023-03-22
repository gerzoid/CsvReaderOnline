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
    static OpenFile(id){
        var formData = new FormData();
        formData.append("fileId", id);
        return axio.post("/api/Files/open", formData);
    }

    //Получить данные
    static GetData(){
        const fileStore = useFileStore();
        var data={
            "FileName" :fileStore.fileInfo.name,
            "PageSize" : fileStore.options.pageSize,
            "Page": fileStore.options.page,
            "Options":fileStore.options
        }
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