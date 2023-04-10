using Entities.Other;
using System.Text.Json.Serialization;

namespace Entities.Answer
{
    public class AnswerGetData
    {
        public Column[]? Columns { get; set; }
        public List<Dictionary<string, object>> Data { get; set; }
    }
}
