using System.Text.Json.Serialization;

namespace TravelApp.Dtos
{
    public class OsmResponseDto
    {
        [JsonPropertyName("elements")]
        public List<OsmElement> Elements { get; set; } = new();
    }

    public class OsmElement 
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("Lat")]
        public double Lat { get; set; }
        
        [JsonPropertyName("Lon")]
        public double Lon { get; set; }
        
        [JsonPropertyName("tags")]
        public Dictionary<string, string> Tags { get; set; } = new();
        
        public string Name => Tags.ContainsKey("name") ? Tags["name"] : "Unnamed place";
    }
}
