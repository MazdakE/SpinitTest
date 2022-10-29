using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpinitTest.Entities
{
    public class StateEntity
    {
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
