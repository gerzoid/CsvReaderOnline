using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Query
{
    public class QuerySettings
    {
        public bool? HasHeader { get; set; } = false;
        public string? Separator { get; set; } =String.Empty;
        public string? Encoding { get; set; } = "ascii";
        public bool? IgnoreBlankLines { get; set; } = true;
        public bool? AllowComments { get; set; } = false;
        public bool? IgnoreQuotes { get; set; } = true;
        public CsvHelper.Configuration.TrimOptions? TrimimOptions { get; set; } = CsvHelper.Configuration.TrimOptions.None;
        public char? Escape { get; set; } = '"';
        public CsvHelper.CsvMode? Mode { get; set; } = CsvHelper.CsvMode.RFC4180;
    }
}
