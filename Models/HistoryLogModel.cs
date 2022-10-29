using Newtonsoft.Json;

namespace SpinitTest.Models
{
    public class HistoryLogModel
    {
        public DateTime SearchDate { get; set; }
        public string? OrderBy { get; set; }
        
        [JsonProperty("Id State")]
        public string? IdState { get; set; }
        public string? State { get; set; }
        [JsonProperty("Id Year")]
        public int IdYear { get; set; }
        public string? Year { get; set; }
        public int Population { get; set; }
        [JsonProperty("Slug State")]
        public string? SlugState { get; set; }
    }
}
