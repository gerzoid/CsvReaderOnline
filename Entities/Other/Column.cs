using System.Text.Json.Serialization;

namespace Entities.Other
{
    public class Column
    {
        [JsonPropertyName("data")]
        public string Name { get; set; }
    }
}
