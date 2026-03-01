using TravelApp.Dtos;
using TravelApp.Models;

namespace TravelApp.Services.Interfaces
{
    public interface ITripService
    {
        Task<bool> CreateTripPlanAsync(CreateTripPlanDto dto);
        Task<bool> AddPlaceToTripPlanAsync(AddPlaceToTripPlanDto dto);
        Task<IEnumerable<TripPlanPlaceDataDto?>> GetTripPlanPlacesByIdAsync(Guid userId, Guid tripPlanId);
        Task<IEnumerable<TripPlansDataDto>> GetAllTripPlansByUserIdAsync(Guid userId);
        Task<bool> DeletePlaceFromTripPlanAsync(Guid tripPlaceId);
        Task<bool> DeleteTripPlanAsync(Guid userId, Guid tripPlanId);
        Place AddPlaceToDatabase(Guid id, string name, double lat, double lon, string osmId);
        bool AddTripPlaceToTripPlan(Guid tripPlanId, Guid placeId, DateTime visitDate);
    }
}
