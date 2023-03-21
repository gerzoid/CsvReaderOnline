using Contracts;
using CsvHelper;
using CsvHelper.Configuration;
using Entities.Other;
using Entities.Query;
using Utils;
using System.Globalization;

namespace CsvService
{
    public class CsvService: ICsvService
    {
        public CsvFileInfo OpenFile(string path, bool hasHeader = true)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = hasHeader,
                DetectDelimiter = true,
                IgnoreBlankLines = true,                
                //Encoding = Encoding.GetEncoding(201),
                TrimOptions = TrimOptions.Trim,
            };
            int countRecords = 0;
            
            CsvFileInfo fileInfo = new CsvFileInfo();            
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, config))
            {

                if (csv.Read())
                {
                    fileInfo.CountColumns = csv.Parser.Count;
                    if (hasHeader)
                        fileInfo.Columns = csv.Parser.Record.Select(p => new Column() { Name = p }).ToArray();
                    else
                    {
                        fileInfo.Columns = Enumerable.Range(1, fileInfo.CountColumns).Select(p => new Column() { Name = "Column" + p }).ToArray();
                        countRecords++;
                    }
                }
                while (csv.Read())
                {
                    countRecords++;
                }
                fileInfo.CountRecords = countRecords;               
            }
            return fileInfo;
        }

        public CsvFileInfo GetData(QueryGetData queryData)
        {
            string path = Helper.GetUploadPathFolder(queryData.FileName);
            
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = queryData.Options.HasHeader,
                DetectDelimiter = true,
                IgnoreBlankLines = true,
                //Encoding = Encoding.GetEncoding(201),
                TrimOptions = TrimOptions.Trim,
            };
            int countRecords = 0;

            CsvFileInfo fileInfo = new CsvFileInfo();
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, config))
            {
                if (csv.Read())
                {
                }
                while (csv.Read())
                {
                    countRecords++;
                }
                fileInfo.CountRecords = countRecords;
            }
            return fileInfo;
        }
    }
}