using System.Security.Claims;
using TravelApp.Dtos;

namespace TravelApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDataDto>> GetAllUsersAsync();
        Task<(UserDataDto? dto, string? error)> GetUserByIdAsync(Guid id, ClaimsPrincipal principal);
        Task<bool> RegisterAsync(CreateUserFormDto dto);
        Task<bool> ChangeUserDataAsync(Guid id, ChangeUserDataDto dto);
    }
}
