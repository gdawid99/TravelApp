using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelApp.Dtos;
using TravelApp.Services.Interfaces;
using TravelApp.Utils;

namespace TravelApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) { _userService = userService; }


        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userService.GetAllUsersAsync();
            return Ok(result);
        }

        [Authorize(Roles = "ADMIN,USER")]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var result = await _userService.GetUserByIdAsync(id, User);

            if (result.error != null)
            {
                return result.error switch
                {
                    "Unauthorized" => Unauthorized(),
                    "Forbidden" => Forbid(),
                    "NotFound" => NotFound(),
                    _ => BadRequest()
                };
            }

            return Ok(result.dto);
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddNewUser(CreateUserFormDto dto)
        {
            var result = await _userService.RegisterAsync(dto);
            return result ? Ok("New account created.") : BadRequest();
        }

        [Authorize(Roles = "ADMIN,USER")]
        [HttpPatch("change/{id:guid}")]
        public async Task<IActionResult> ChangeUserData(ChangeUserDataDto dto)
        {
            var id = User.GetUserId();
            var result = await _userService.ChangeUserDataAsync(id, dto);
            return result ? Ok("USER data has been changed.") : NotFound("USER not exist.");
        }
    }
}
