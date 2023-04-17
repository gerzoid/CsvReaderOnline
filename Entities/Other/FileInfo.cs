namespace Entities.Other
{
    public class FileInfo
    {
        public string FileName { get; set; }
        public int CountRecords { get; set; }
        public int CountColumns { get; set; }
        public Column[]? Columns { get; set; } 
    }
}
