using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TravelApp.Models;
using TravelApp.Enums;
using TravelApp.Dtos;
using TravelApp.Data;
using System.Security.Cryptography;

namespace TravelApp.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;
        private readonly TravelAppDbContext _context;

        public TokenService(IConfiguration config, TravelAppDbContext context)
        {
            _config = config;
            _context = context;
        }


        public string InitToken(User user, DateTime expires, IEnumerable<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthConfiguration:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            var token = new JwtSecurityToken(
                issuer: _config["AuthConfiguration:Issuer"],
                audience: _config["AuthConfiguration:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public IEnumerable<Claim> GetClaims(User user)
        {
            return new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, ((RoleEnum) user.Role).ToString())
            };
        }

        public (string token, DateTime expiresAt) GenerateAccessToken(User user)
        {
            var expires = DateTime.UtcNow.AddMinutes(5);
            var claims = GetClaims(user);
            return (InitToken(user, expires, claims), expires);
        }

        public (string token, DateTime expiresAt) GenerateRefreshToken(User user)
        {
            var expires = DateTime.UtcNow.AddDays(10);
            var claims = GetClaims(user);
            return (InitToken(user, expires, claims), expires);
        }

        public async Task<AuthResponseDto> GenerateTokenAsync(User user)
        {
            var accessTokenRes = GenerateAccessToken(user);
            var refreshTokenRes = GenerateRefreshToken(user);

            user.RefreshToken = HashToken(refreshTokenRes.token);
            user.RefreshTokenExpiresAt = refreshTokenRes.expiresAt;
            user.RefreshTokenCreatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                AccessToken = accessTokenRes.token,
                AccessTokenExpiresAt = accessTokenRes.expiresAt,
                RefreshToken = refreshTokenRes.token,
                RefreshTokenExpiresAt = refreshTokenRes.expiresAt,
                UserData = new UserDataDto
                {
                    Id = user.UserId,
                    Name = user.Name,
                    Email = user.Email,
                    LoginDate = user.LoginDate,
                    Status = user.Status,
                    Role = user.Role
                }
            };
        }

        public string HashToken(string token)
        {
            var hash = SHA256.Create();
            var bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(token));
            return Convert.ToBase64String(bytes);
        }
    }
}
