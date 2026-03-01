using Microsoft.AspNetCore.Mvc;
using TravelApp.Dtos;
using TravelApp.Services.Interfaces;
using TravelApp.Utils;

namespace TravelApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TripController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripController(ITripService tripService) { _tripService = tripService; }

        [HttpPost("create")]
        public async Task<IActionResult> CreateTripPlan(CreateTripPlanDto dto)
        {
            return await _tripService.CreateTripPlanAsync(dto) ? Ok() : BadRequest();
        }

        [HttpPost("add/place")]
        public async Task<IActionResult> AddNewPlaceToTripPlan(AddPlaceToTripPlanDto dto)
        {
            return await _tripService.AddPlaceToTripPlanAsync(dto) ? Ok() : BadRequest();
        }

        [HttpGet("{tripPlanId:guid}")]
        public async Task<IActionResult> GetTripPlanPlaces(Guid tripPlanId)
        {
            var userId = User.GetUserId();
            var result = await _tripService.GetTripPlanPlacesByIdAsync(userId, tripPlanId);
            if (result is null) return NotFound();
            else return Ok(result);
        }

        [HttpGet("tripplans")]
        public async Task<IActionResult> GetAllTripPlans()
        {
            var userId = User.GetUserId();

            var result = await _tripService.GetAllTripPlansByUserIdAsync(userId);
            return Ok(result);
        }

        [HttpDelete("place/delete/{tripPlaceId:guid}")]
        public async Task<IActionResult> DeleteTripPlace(Guid tripPlaceId)
        {
            return await _tripService.DeletePlaceFromTripPlanAsync(tripPlaceId) ? NoContent() : NotFound();
        }

        [HttpDelete("tripplan/delete/{tripPlanId:guid}")]
        public async Task<IActionResult> DeleteTripPlan(Guid tripPlanId)
        {
            return await _tripService.DeleteTripPlanAsync(User.GetUserId(), tripPlanId) ? NoContent() : NotFound();
        }
    }
}
