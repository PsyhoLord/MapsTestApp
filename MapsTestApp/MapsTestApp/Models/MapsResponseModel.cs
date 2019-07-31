using System.Collections.Generic;
using Newtonsoft.Json;

namespace MapsTestApp.Models
{
    public class MapsResponseModel
    {
        [JsonProperty("candidates")]
        public List<Candidate> Candidates { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class Location
    {
        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("lng")]
        public double Longitude { get; set; }
    }

    public class Geometry
    {
        [JsonProperty("location")]
        public Location Location { get; set; }
    }

    public class Candidate
    {
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
