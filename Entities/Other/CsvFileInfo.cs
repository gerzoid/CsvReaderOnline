namespace Entities.Other
{
    public class CsvFileInfo
    {
        public string FileName { get; set; }
        public int CountRecords { get; set; }
        public int CountColumns { get; set; }
        public string Separator { get; set; }
        public bool HasHeader { get ; set; }
        public Column[]? Columns { get; set; } 
    }
}
