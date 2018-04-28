using Newtonsoft.Json;

namespace ClujBikeLite.Models
{
    public class StationsData
    {
        [JsonProperty("Data")]
        public Station[] Data { get; set; }

        [JsonProperty("Total")]
        public long Total { get; set; }

        [JsonProperty("AggregateResults")]
        public object AggregateResults { get; set; }

        [JsonProperty("Errors")]
        public object Errors { get; set; }
    }
}