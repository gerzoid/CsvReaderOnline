using Contracts;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Text;

namespace CsvService
{
    public class CsvService: ICsvService
    {
        public bool OpenFile(string path)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                DetectDelimiter = true,
                IgnoreBlankLines = true,
                Encoding = Encoding.GetEncoding(201),
                TrimOptions = TrimOptions.Trim,
            };

            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, config))
            {
                while (csv.Read())
                {


                    var c = csv.Context;
                    var s = c.Reader.Parser.RawRow;
                    var s1 = c.Reader.Parser.RawRecord;
                    var s2 = c.Parser.RawRecord.ToString();
                    var s3 = c.Parser.Record;

                    var cs = new CsvParser(new StringReader(s1), config);
                    while (true)
                    {
                        var read = cs.Read();
                        if (!read)
                            break;

                    }
                }

               // var records = c.Reader.GetRecords<dynamic>().ToArray(); // если структура файла неизвестна                                                         // или                                                         
            }
            return false;
        }
    }
}