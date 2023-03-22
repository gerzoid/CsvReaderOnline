namespace Entities.Query
{
    public class QueryGetData
    {
        public string FileName { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
        public QueryOptions? Options { get; set; }
    }
}
