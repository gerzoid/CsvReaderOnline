namespace Entities.Query
{
    public class QueryGetData
    {
        public string FileName { get; set; }
        public int CountRows {  get; set; }
        public int CountColumns { get; set; }
        public QueryOptions? Options { get; set; }
    }
}
