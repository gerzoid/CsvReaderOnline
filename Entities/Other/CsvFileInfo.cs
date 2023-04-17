using Entities.Query;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Other
{
    public class CsvFileInfo
    {
        public CsvFileInfo()
        {
            Info = new FileInfo();
            Settings = new QuerySettings();
        }

        public FileInfo Info { get; set; }
        public QuerySettings Settings { get; set; }
    }

   
}
