using WebAPI_NOVOAssignment.DTOs;
using WebAPI_NOVOAssignment.Models;
using WebAPI_NOVOAssignment.Repositories.Interfaces;
using WebAPI_NOVOAssignment.Services.Interfaces;
using WebAPI_NOVOAssignment.Utilities;

namespace WebAPI_NOVOAssignment.Services;

/// <summary>
/// Service implementation for user management operations
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    /// <summary>
    /// Initializes a new instance of the UserService
    /// </summary>
    public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

   
    public async Task<(List<UserDetailDto>, int)> GetAllUsersAsync(int pageNumber = 1, int pageSize = 10)
    {
        var (users, totalCount) = await _userRepository.GetPaginatedAsync(pageNumber, pageSize);
        var userDtos = users.Select(MapToDetailDto).ToList();
        return (userDtos, totalCount);
    }

   
    public async Task<UserDetailDto?> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user == null ? null : MapToDetailDto(user);
    }

   
    public async Task<UserDetailDto?> CreateUserAsync(CreateUserRequestDto request)
    {
        // Validate input
        if (string.IsNullOrWhiteSpace(request.Username) || request.Username.Length < 3 || request.Username.Length > 20)
            return null;

        if (string.IsNullOrWhiteSpace(request.Email) || !IsValidEmail(request.Email))
            return null;

        if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 8)
            return null;

        // Check if email or username already exists
        if (await _userRepository.EmailExistsAsync(request.Email) || 
            await _userRepository.UsernameExistsAsync(request.Username))
            return null;

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            Email = request.Email,
            PasswordHash = PasswordHasher.HashPassword(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        // Assign roles
        if (request.RoleIds.Count > 0)
        {
            var roles = new List<Role>();
            foreach (var roleId in request.RoleIds)
            {
                var role = await _roleRepository.GetByIdAsync(roleId);
                if (role != null)
                    roles.Add(role);
            }
            user.Roles = roles;
        }

        var createdUser = await _userRepository.AddAsync(user);
        return MapToDetailDto(createdUser);
    }

   
    public async Task<UserDetailDto?> UpdateUserAsync(Guid id, UpdateUserRequestDto request)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return null;

        // Validate input
        if (string.IsNullOrWhiteSpace(request.Username) || request.Username.Length < 3 || request.Username.Length > 20)
            return null;

        if (string.IsNullOrWhiteSpace(request.Email) || !IsValidEmail(request.Email))
            return null;

        // Check if email or username already exists for other users
        if (user.Email != request.Email && await _userRepository.EmailExistsAsync(request.Email))
            return null;

        if (user.Username != request.Username && await _userRepository.UsernameExistsAsync(request.Username))
            return null;

        user.Username = request.Username;
        user.Email = request.Email;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.UpdatedDate = DateTime.UtcNow;

        // Update roles
        user.Roles.Clear();
        if (request.RoleIds.Count > 0)
        {
            var roles = new List<Role>();
            foreach (var roleId in request.RoleIds)
            {
                var role = await _roleRepository.GetByIdAsync(roleId);
                if (role != null)
                    roles.Add(role);
            }
            user.Roles = roles;
        }

        var updatedUser = await _userRepository.UpdateAsync(user);
        return MapToDetailDto(updatedUser);
    }

   
    public async Task<bool> DeleteUserAsync(Guid id)
    {
        return await _userRepository.DeleteAsync(id);
    }

    private static UserDetailDto MapToDetailDto(User user)
    {
        return new UserDetailDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            CreatedDate = user.CreatedDate,
            Roles = user.Roles.Select(r => r.Name).ToList()
        };
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
