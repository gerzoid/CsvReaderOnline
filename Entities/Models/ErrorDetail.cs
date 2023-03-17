using Newtonsoft.Json;

namespace CSVReader.Models {
    public class ErrorDetails {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}