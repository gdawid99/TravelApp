using Microsoft.AspNetCore.Mvc;
using TravelApp.Dtos;
using TravelApp.Services.Interfaces;

namespace TravelApp.Controllers
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class OsmController : ControllerBase
    {
        public readonly IOsmService _osmService;

        public OsmController(IOsmService osmService) {
            _osmService = osmService;
        }

        [HttpPost("data")]
        public async Task<IActionResult> GetDataFromOsm(OsmDataDto dto)
        {
            var res = await _osmService.GetPlacesAsync(dto.Lat, dto.Lon);
            return res is null ? NotFound() : Ok(res);
        }
    }
}
