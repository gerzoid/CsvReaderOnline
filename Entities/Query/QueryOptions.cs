using System.Globalization;

namespace Entities.Query
{
    public class QueryOptions
    {
        public bool HasHeader { get; set; }
        public int Page {  get; set; }
        public int PageSize { get; set; }
        public string Separator { get; set; }
        public string Encoding{ get; set; }
    }
}
