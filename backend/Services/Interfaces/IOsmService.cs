using TravelApp.Dtos;

namespace TravelApp.Services.Interfaces
{
    public interface IOsmService
    {
        Task<OsmResponseDto?> GetPlacesAsync(double lat, double lon);
    }
}
