using WebAPI_NOVOAssignment.DTOs;

namespace WebAPI_NOVOAssignment.Services.Interfaces;

/// <summary>
/// Service interface for role management operations
/// </summary>
public interface IRoleService
{
    Task<(List<RoleDetailDto>, int)> GetAllRolesAsync(int pageNumber = 1, int pageSize = 10);
    Task<RoleDetailDto?> GetRoleByIdAsync(Guid id);
    Task<RoleDetailDto?> CreateRoleAsync(CreateRoleRequestDto request);
    Task<RoleDetailDto?> UpdateRoleAsync(Guid id, UpdateRoleRequestDto request);
    Task<bool> DeleteRoleAsync(Guid id);
}
