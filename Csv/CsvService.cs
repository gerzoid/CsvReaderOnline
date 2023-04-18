using Contracts;
using CsvHelper;
using CsvHelper.Configuration;
using Entities.Other;
using Entities.Query;
using Utils;
using System.Globalization;
using Contracts.Repository;
using Entities.Models;
using System.Text;
using System.IO;
using System.Reflection;
using Entities.Answer;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Runtime.InteropServices;

namespace CsvService
{
    public class CsvService: ICsvService
    {
        IRepositoryManager _manager;
        public CsvService(IRepositoryManager manager)
        {
            _manager = manager;
        }
        //Загрузка файла на сервер
        public async Task<CsvFileInfo> UploadFile(FileModel file)
        {
            var fileId = Guid.NewGuid();
            var newFile = new Entities.Models.Files()
            {
                CreatedAt = DateTime.Now,
                UserId = Guid.Parse(file.UserId),
                OriginalName = file.FileName,
                FilesId = fileId,
                Size = file.FormFile.Length,
                Path = Helper.GetUploadPathFolder(fileId.ToString() + Path.GetExtension(file.FileName.ToLower())),
                CountColumns = 0,
                CountRows = 0,
                //Path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/upload", fileId.ToString() + Path.GetExtension(file.FileName.ToLower()))
            };

            using (Stream stream = new FileStream(newFile.Path, FileMode.Create))
            {
                await file.FormFile.CopyToAsync(stream);
            }
            
            var info = OpenFile(newFile.Path);

            newFile.Separator = info.Settings.Separator[0];
            newFile.CountColumns = info.Info.CountColumns;
            newFile.CountRows = info.Info.CountRecords;

            _manager.FilesRepository.Create(newFile);
            _manager.Save();
            
            //Удаляем лишние файлы из базы и физически
            CheckCountFileByUserAndDelete(file.UserId);

            return info;
        }

        public void CheckCountFileByUserAndDelete(string userId)
        {
            var files = _manager.FilesRepository.Find(d => d.UserId == Guid.Parse(userId)).OrderByDescending(d => d.CreatedAt).Skip(Helper.COUNT_MAX_UPLOAD_FILES).ToList();
            foreach (var item in files)
            {
                System.IO.File.Delete(item.Path);
                _manager.FilesRepository.Remove(item);
                _manager.Save();
            }            
        }

        //Получить или создать пользователя
        public Users GetOrCreateUser(string userId)
        {
            var user = _manager.UsersRepository.Get().Where(d => d.UsersId.ToString() == userId).SingleOrDefault();

            if (user == null)
            {
                user = new Users() { UsersId = Guid.NewGuid(), Name = "" };
                _manager.UsersRepository.Create(user);
                _manager.Save();
            }
            else
            {
                var files = _manager.FilesRepository.Get().Where(d => d.UserId.ToString() == userId).OrderByDescending(d => d.CreatedAt).ToList();
                user.Files = files;
            }
            return user;
        }


        public CsvFileInfo OpenFile(string fileName, bool hasHeader = true)
        {
            string path = Helper.GetUploadPathFolder(fileName);
            
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                DetectDelimiter = true,
                Quote = '"',
                IgnoreBlankLines = true,
                //Encoding = Encoding.GetEncoding(201),
                TrimOptions = TrimOptions.Trim,
            };
            

            var file = _manager.FilesRepository.Find(d => d.FilesId == Guid.Parse(Path.GetFileNameWithoutExtension(fileName))).FirstOrDefault();
            if (file!=null)
            {
                config.HasHeaderRecord = file.HasHeader;
                config.DetectDelimiter = file.Separator=='\0' ? true : false;
                config.Delimiter = file.Separator == '\0' ? ";" :  Convert.ToString(file.Separator);
                if (file.Encoding != null)
                    config.Encoding = Encoding.GetEncoding(file.Encoding);
            }
            //Атодетект кодировки
            if ((file==null) ||(file.Encoding is null))
            {
                Ude.CharsetDetector charsetDetector = new Ude.CharsetDetector();
                using (FileStream fileStream = File.OpenRead(path))
                {
                    charsetDetector.Feed(fileStream);
                    charsetDetector.DataEnd();
                    config.Encoding = Encoding.GetEncoding(charsetDetector.Charset ?? "Windows-1251");
                }
            }

            int countRecords = 0;
            
            CsvFileInfo fileInfo = new CsvFileInfo();

            fileInfo.Settings.Encoding = config.Encoding.HeaderName;
            fileInfo.Settings.HasHeader = config.HasHeaderRecord;
            fileInfo.Settings.Separator = config.Delimiter;
            fileInfo.Info.FileName = fileName;

            using (var reader = new StreamReader(path, config.Encoding))
            using (var csv = new CsvReader(reader, config))
            {
                if (csv.Read())
                {
                    fileInfo.Info.CountColumns = csv.Parser.Count;
                    if (csv.Configuration.HasHeaderRecord)
                        fileInfo.Info.Columns = csv.Parser.Record.Select(p => new Column() { Name = p, Title=p, Size=50, Type="text"}).ToArray();
                    else
                    {
                        fileInfo.Info.Columns = Enumerable.Range(1, fileInfo.Info.CountColumns).Select(p => new Column() { Name = "Column" + p,Title = "Column" + p, Size = 50, Type = "text" }).ToArray();
                        countRecords++;
                    }
                }
                while (csv.Read())
                {
                    countRecords++;
                }
                fileInfo.Info.CountRecords = countRecords;
                fileInfo.Settings.Separator = csv.Parser.Delimiter;
            }
            return fileInfo;
        }

        //public List<Dictionary<string, object>> GetData(QueryGetData queryData)
        public AnswerGetData GetData(QueryGetData queryData)
        {
            string path = Helper.GetUploadPathFolder(queryData.FileName);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = queryData.Settings.HasHeader ?? false,
                DetectDelimiter = true,
                IgnoreBlankLines = true,
                Encoding = Encoding.GetEncoding(queryData.Settings.Encoding),
                Escape = '|',
                TrimOptions = TrimOptions.Trim,                
                BadDataFound = null,
                Mode = CsvMode.RFC4180
            };

            AnswerGetData answer = new AnswerGetData();

            config.MissingFieldFound = (headerNames) =>
            {
                Console.WriteLine("sdfsd");

            };


            if (queryData.Options.NeedSaveSettings)
            {
                var file = _manager.FilesRepository.Find(d => d.FilesId == Guid.Parse(Path.GetFileNameWithoutExtension(queryData.FileName))).FirstOrDefault();
                file.Separator = queryData.Settings.Separator != "" ? Convert.ToChar(queryData.Settings.Separator) : '\0';
                file.HasHeader = queryData.Settings.HasHeader ?? false;
                file.Encoding = queryData.Settings.Encoding;
                _manager.Save();
            }

            if ((queryData.Settings.Separator != null) && (queryData.Settings.Separator != ""))
            {
                config.DetectDelimiter = false;
                config.Delimiter = queryData.Settings.Separator;
            }

            answer.Data = new List<Dictionary<string, object>>();
            
              int startRow = queryData.Options.PageSize * (queryData.Options.Page - 1);
              startRow = startRow >= queryData.CountRows? queryData.CountRows : startRow;
              int endRow = startRow + queryData.Options.PageSize > queryData.CountRows ? queryData.CountRows : startRow + queryData.Options.PageSize;

            using (var reader = new StreamReader(path, config.Encoding))
            //using (var reader = new StreamReader(path, Encoding.GetEncoding(queryData.Settings.Encoding)))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                Dictionary<string, object> values = new Dictionary<string, object>();
                if (queryData.Settings.HasHeader ?? false)
                    answer.Columns = csv.Parser.Record.Select(p => new Column() { Name = p.Trim(), Title = p.Trim(), Size = 50, Type = "text" }).ToArray();
                else
                {
                    answer.Columns = Enumerable.Range(1, csv.Parser.Count).Select(p => new Column() { Name = "Column" + p, Title = "Column" + p, Size = 50, Type = "text" }).ToArray();

                    //Хак. Каостыль. Добавили первую строку, котороую прочитали, не нашел как вернуться на 0 позицию для чтения в csvhelper
                    //если это первая страница, то отображаем первую запись
                    if (startRow == 0)
                    {
                        for (int i = 0; i < answer.Columns.Count(); i++)
                            values.Add(answer.Columns[i].Name, csv[i].ToString());
                        answer.Data.Add(values);
                    }
                }


                for (var i = queryData.Settings.HasHeader ?? false ? 1 : 0; i < startRow; i++)
                    csv.Read();
                

                for (int x = startRow; x < endRow; x++)
                {
                    csv.Read();

                    values = new Dictionary<string, object>();
                    for (int i = 0; i < answer.Columns.Count(); i++)
                    {
                        values.Add(answer.Columns[i].Name, csv[i].ToString());
                    }
                    answer.Data.Add(values);
                }
                return answer;
            }
        }
    }
}