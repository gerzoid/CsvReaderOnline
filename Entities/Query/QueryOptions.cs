using System.Globalization;

namespace Entities.Query
{
    public class QueryOptions
    {
        public int Page {  get; set; }
        public int PageSize { get; set; }
        public bool NeedSaveSettings { get; set; }
    }
}
