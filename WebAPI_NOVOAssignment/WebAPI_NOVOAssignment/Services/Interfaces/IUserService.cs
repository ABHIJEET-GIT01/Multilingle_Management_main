using WebAPI_NOVOAssignment.DTOs;

namespace WebAPI_NOVOAssignment.Services.Interfaces;

/// <summary>
/// Service interface for user management operations
/// </summary>
public interface IUserService
{
    Task<(List<UserDetailDto>, int)> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10);
    Task<UserDetailDto?> GetUserByIdAsync(Guid id);
    Task<UserDetailDto?> CreateUserAsync(CreateUserRequestDto request);
    Task<UserDetailDto?> UpdateUserAsync(Guid id, UpdateUserRequestDto request);

    Task<bool> DeleteUserAsync(Guid id);
}
