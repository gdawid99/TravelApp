using Microsoft.EntityFrameworkCore;
using TravelApp.Data;
using TravelApp.Dtos;
using TravelApp.Models;
using TravelApp.Services.Interfaces;

namespace TravelApp.Services

{
    public class TripService : ITripService
    {
        private readonly TravelAppDbContext _context;
        
        public TripService(TravelAppDbContext context) { _context = context; }

        public async Task<bool> CreateTripPlanAsync(CreateTripPlanDto dto)
        {

            var user = await _context.Users.FindAsync(dto.UserId);

            if (user is null) {
                return false;
            }


            var tripPlan = new TripPlan
            {
                Title = dto.Title,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                User = user,
                UserId = dto.UserId
            };

            _context.TripPlans.Add(tripPlan);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddPlaceToTripPlanAsync(AddPlaceToTripPlanDto dto)
        {

            bool isTripPlanExist = await _context.TripPlans.AnyAsync(x => x.TripPlanId == dto.TripPlanId);
            
            if (!isTripPlanExist) return false;

            var place = await _context.Places.FirstOrDefaultAsync(x => x.OsmId == dto.OsmId);

            if (place is null)
            {
                place = AddPlaceToDatabase(Guid.NewGuid(), dto.Name, dto.Latitude, dto.Longitude, dto.OsmId);
            }

            AddTripPlaceToTripPlan(dto.TripPlanId, place.PlaceId, dto.VisitDate);

            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        public async Task<IEnumerable<TripPlanPlaceDataDto?>> GetTripPlanPlacesByIdAsync(Guid userId, Guid tripPlanId)
        {
            bool isCorrectUser = await _context.TripPlans.AnyAsync(x => x.TripPlanId == tripPlanId && x.UserId == userId);
            if (!isCorrectUser) 
            {
                return null;
            }

            var result = await _context.TripPlaces.Where(x => x.TripPlanId == tripPlanId).Select(u => new TripPlanPlaceDataDto
            {
                Name = u.Place.Name,
                Note = u.Note,
                VisitDate = u.VisitDate,
            }).OrderBy(x => x.VisitDate).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<TripPlansDataDto>> GetAllTripPlansByUserIdAsync(Guid userId)
        {
            return await _context.TripPlans
                .Where(tp => tp.UserId == userId)
                .Select(tp => new TripPlansDataDto
                {
                    TripPlanId = tp.TripPlanId,
                    Title = tp.Title,
                    Places = tp.TripPlaces
                        .OrderBy(tp => tp.VisitDate)
                        .Select(p => new TripPlanPlaceDataDto
                        {
                            Name = p.Place.Name,
                            Note = p.Note,
                            VisitDate = p.VisitDate,
                            Latitude = p.Place.Latitude,
                            Longitude = p.Place.Longitude
                        })
                })
                .ToListAsync();
        }

        public async Task<bool> DeletePlaceFromTripPlanAsync(Guid tripPlaceId)
        {
            var res = await _context.TripPlaces.Where(tp => tp.TripPlaceId == tripPlaceId).ExecuteDeleteAsync();
            return res > 0;
        }

        public async Task<bool> DeleteTripPlanAsync(Guid userId, Guid tripPlanId)
        {
            var res = await _context.TripPlans.Where(t => t.TripPlanId == tripPlanId && t.UserId == userId).ExecuteDeleteAsync();
            return res > 0;
        }


        public Place AddPlaceToDatabase(Guid id, string name, double lat, double lon, string osmId)
        {
            Place place = new Place
            {
                PlaceId = id,
                Name = name,
                Latitude = lat,
                Longitude = lon,
                OsmId = osmId
            };

            _context.Places.Add(place);
            return place;
        }

        public bool AddTripPlaceToTripPlan(Guid tripPlanId, Guid placeId, DateTime visitDate)
        {
            TripPlace tripPlace = new TripPlace
            {
                PlaceId = placeId,
                TripPlanId = tripPlanId,
                VisitDate = visitDate
            };

            _context.TripPlaces.Add(tripPlace);
            return true;
        }
    }
}
