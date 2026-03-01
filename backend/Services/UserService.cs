using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TravelApp.Data;
using TravelApp.Dtos;
using TravelApp.Services.Interfaces;

namespace TravelApp.Services
{
    public class UserService : IUserService
    {
        private readonly TravelAppDbContext _context;
        public UserService(TravelAppDbContext context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDataDto>> GetAllUsersAsync()
        {
            var users = await _context.Users
                .Select(u => new UserDataDto
                {
                    Id = u.UserId,
                    Name = u.Name,
                    Email = u.Email,
                    LoginDate = u.LoginDate,
                    Status = u.Status,
                    Role = u.Role
                })
                .ToListAsync();
            return users;
        }

        public async Task<(UserDataDto? dto, string? error)> GetUserByIdAsync(Guid id, ClaimsPrincipal principal)
        {
            var tokenUserRole = principal.FindFirstValue(ClaimTypes.Role);

            if (tokenUserRole == "USER")
            {
                var tokenUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
                if (tokenUserId == null)
                {
                    return (null, "Unauthorized");
                }

                if (tokenUserId != id.ToString())
                {
                    return (null, "Forbid");
                }
            }

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return (null, "NotFound");
            }

            var dto = new UserDataDto
            {
                Id = user.UserId,
                Name = user.Name,
                Email = user.Email,
                LoginDate = user.LoginDate,
                Status = user.Status,
                Role = user.Role
            };

            return (dto, null);
        }

        public async Task<bool> RegisterAsync(CreateUserFormDto dto)
        {
            bool isEmailInDatabase = await _context.Users.AnyAsync(u => u.Email == dto.Email);

            if (isEmailInDatabase)
            {
                return false;
            }

            var hasher = new PasswordHasher<Models.User>();

            var user = new Models.User
            {
                Name = dto.Name,
                Email = dto.Email
            };

            user.Password = hasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangeUserDataAsync(Guid id, ChangeUserDataDto dto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(dto.Name))
                user.Name = dto.Name;

            if (!string.IsNullOrEmpty(dto.Email))
                user.Email = dto.Email;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
