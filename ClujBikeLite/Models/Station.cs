
using Newtonsoft.Json;

namespace ClujBikeLite.Models
{
    public class Station
    {
        [JsonProperty("StationName")]
        public string StationName { get; set; }

        [JsonProperty("Address")]
        public string Address { get; set; }

        [JsonProperty("OcuppiedSpots")]
        public long OcuppiedSpots { get; set; }

        [JsonProperty("EmptySpots")]
        public long EmptySpots { get; set; }

        [JsonProperty("MaximumNumberOfBikes")]
        public long MaximumNumberOfBikes { get; set; }

        [JsonProperty("LastSyncDate")]
        public string LastSyncDate { get; set; }

        [JsonProperty("IdStatus")]
        public long IdStatus { get; set; }

        [JsonProperty("Status")]
        public string Status { get; set; }

        [JsonProperty("StatusType")]
        public string StatusType { get; set; }

        [JsonProperty("Latitude")]
        public double Latitude { get; set; }

        [JsonProperty("Longitude")]
        public double Longitude { get; set; }

        [JsonProperty("IsValid")]
        public bool IsValid { get; set; }

        [JsonProperty("CustomIsValid")]
        public bool CustomIsValid { get; set; }

        [JsonProperty("Notifies")]
        public object[] Notifies { get; set; }

        [JsonProperty("Id")]
        public long Id { get; set; }
    }
}