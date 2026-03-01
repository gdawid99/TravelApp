using System.Globalization;
using TravelApp.Dtos;
using TravelApp.Services.Interfaces;

namespace TravelApp.Services
{
    public class OsmService : IOsmService
    {
        private readonly HttpClient _httpClient;

        public OsmService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OsmResponseDto?> GetPlacesAsync(double lat, double lon)
        {
            var Uri = new Uri("https://overpass-api.de/api/interpreter");

            string sLat = lat.ToString(CultureInfo.InvariantCulture);
            string sLon = lon.ToString(CultureInfo.InvariantCulture);

            string query = 
                $"[out:json];" +
                $"nwr[\"tourism\"=\"museum\"](around:5000, {sLat}, {sLon});" +
                $"out body;";

            var values = new Dictionary<string, string>
            {
                { "data", query }
            };

            var content = new FormUrlEncodedContent(values);

            var res = await _httpClient.PostAsync(Uri, content);
            res.EnsureSuccessStatusCode();

            var data = await res.Content.ReadFromJsonAsync<OsmResponseDto>();

            return data;
        }
    }
}
