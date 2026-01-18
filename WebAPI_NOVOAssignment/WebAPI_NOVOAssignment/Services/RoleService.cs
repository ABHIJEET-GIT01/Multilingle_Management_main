using WebAPI_NOVOAssignment.DTOs;
using WebAPI_NOVOAssignment.Models;
using WebAPI_NOVOAssignment.Repositories.Interfaces;
using WebAPI_NOVOAssignment.Services.Interfaces;

namespace WebAPI_NOVOAssignment.Services;

/// <summary>
/// Service implementation for role management operations
/// </summary>
public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    /// <summary>
    /// Initializes a new instance of the RoleService
    /// </summary>
    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

   
    public async Task<(List<RoleDetailDto>, int)> GetAllRolesAsync(int pageNumber = 1, int pageSize = 10)
    {
        var (roles, totalCount) = await _roleRepository.GetPaginatedAsync(pageNumber, pageSize);
        var roleDtos = roles.Select(MapToDetailDto).ToList();
        return (roleDtos, totalCount);
    }

   
    public async Task<RoleDetailDto?> GetRoleByIdAsync(Guid id)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        return role == null ? null : MapToDetailDto(role);
    }

   
    public async Task<RoleDetailDto?> CreateRoleAsync(CreateRoleRequestDto request)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length < 3 || request.Name.Length > 50)
            return null;

        if (!string.IsNullOrEmpty(request.Description) && request.Description.Length > 500)
            return null;

        // Check if role name already exists
        if (await _roleRepository.RoleNameExistsAsync(request.Name))
            return null;

        var role = new Role
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Description = request.Description,
            CreatedDate = DateTime.UtcNow
        };

        var createdRole = await _roleRepository.AddAsync(role);
        return MapToDetailDto(createdRole);
    }

   
    public async Task<RoleDetailDto?> UpdateRoleAsync(Guid id, UpdateRoleRequestDto request)
    {
        var role = await _roleRepository.GetByIdAsync(id);
        if (role == null)
            return null;

        // Validate input
        if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length < 3 || request.Name.Length > 50)
            return null;

        if (!string.IsNullOrEmpty(request.Description) && request.Description.Length > 500)
            return null;

        // Check if new name already exists for other roles
        if (role.Name != request.Name && await _roleRepository.RoleNameExistsAsync(request.Name))
            return null;

        role.Name = request.Name;
        role.Description = request.Description;

        var updatedRole = await _roleRepository.UpdateAsync(role);
        return MapToDetailDto(updatedRole);
    }

   
    public async Task<bool> DeleteRoleAsync(Guid id)
    {
        return await _roleRepository.DeleteAsync(id);
    }

    private static RoleDetailDto MapToDetailDto(Role role)
    {
        return new RoleDetailDto
        {
            Id = role.Id,
            Name = role.Name,
            Description = role.Description,
            CreatedDate = role.CreatedDate
        };
    }
}
