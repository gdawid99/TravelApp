using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelApp.Dtos;
using TravelApp.Models;
using TravelApp.Services;

namespace TravelApp.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly Data.TravelAppDbContext _context;
        private readonly TokenService _tokenService;
        public AuthController(Data.TravelAppDbContext context, TokenService tokenService) 
        { 
            _context = context; 
            _tokenService = tokenService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginUser(LoginDataDto dto)
        {
            var failedMessage = "Invalid e-mail and/or password.";

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if (user is null)
            {
                return Unauthorized(failedMessage);
            }

            var passwordHasher = new PasswordHasher<User>();
            var verify = passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);

            if (verify == PasswordVerificationResult.Failed)
            {
                return Unauthorized(failedMessage);
            }

            var res = await _tokenService.GenerateTokenAsync(user);

            Response.Cookies.Append("refreshToken", res.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, //temporarily, remember to change when the protocol switched to https
                SameSite = SameSiteMode.Strict,
                Expires = res.RefreshTokenExpiresAt
            });

            return Ok(new
            {
                accessToken = res.AccessToken,
                userData = user
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken() 
        {

            if (!Request.Cookies.TryGetValue("refreshToken", out var refreshToken))
            {
                return Unauthorized("Refresh token not found.");
            }

            var hashedRefreshToken = _tokenService.HashToken(refreshToken);

            var user = await _context.Users.Where(u => u.RefreshToken == hashedRefreshToken).FirstOrDefaultAsync();

            if (user is null || user.RefreshTokenExpiresAt < DateTime.UtcNow)
            {
                return Unauthorized("Invalid or expired refresh token.");
            }

            var res = await _tokenService.GenerateTokenAsync(user);

            Response.Cookies.Append("refreshToken", res.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, //temporarily, remember to change when the protocol switched to https
                SameSite = SameSiteMode.Strict,
                Expires = res.RefreshTokenExpiresAt
            });

            return Ok(new
            {
                accessToken = res.AccessToken,
                userData = user
            });
        }
    }
}
