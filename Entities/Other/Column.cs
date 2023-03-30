using System.Text.Json.Serialization;

namespace Entities.Other
{
    public class Column
    {
        [JsonPropertyName("data")]
        public string? Name { get; set; }
        public string? Type { get; set; }   //Тип для Handsontable
        public string? Title { get; set; }
        [JsonPropertyName("width1")]
        public int Size { get; set; }   //Размер для Handsontable, становится width

    }
}
