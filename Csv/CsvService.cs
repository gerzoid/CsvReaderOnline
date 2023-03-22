using Contracts;
using CsvHelper;
using CsvHelper.Configuration;
using Entities.Other;
using Entities.Query;
using Utils;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var rows = new List<Dictionary<string, object>>();

            /*  int startRow = queryData.PageSize * (queryData.Page - 1);
              startRow = startRow >= dbf.CountRows ? dbf.CountRows : startRow;
              int endRow = startRow + queryData.PageSize > dbf.CountRows ? dbf.CountRows : startRow + queryData.PageSize;

              using (var reader = new StreamReader(path))
              using (var csv = new CsvReader(reader, config))
              {
                  for (int indexRow = queryData.startRow; indexRow < endRow; indexRow++)
                  {
                      Dictionary<string, object> values = new Dictionary<string, object>();
                      for (int i = 0; i < dbf.CountColumns - 1; i++)
                      {
                          values.Add(dbf.GetColumnName(i), dbf.GetValue(i, indexRow));
                      }
                      values.Add("_IS_DELETED_", dbf.IsDeleted(indexRow));
                      rows.Add(values);
                  }

                  if (csv.Read())
                  {
                  }
                  while (csv.Read())
                  {
                      countRecords++;
                  }
                  fileInfo.CountRecords = countRecords;
              }*/
            return null;
        }
    }
}