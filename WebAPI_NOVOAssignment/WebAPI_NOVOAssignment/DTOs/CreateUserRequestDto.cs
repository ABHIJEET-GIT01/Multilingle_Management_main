namespace WebAPI_NOVOAssignment.DTOs;

/// <summary>
/// Request DTO for creating a new user
/// </summary>
public class CreateUserRequestDto
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public List<Guid> RoleIds { get; set; } = [];
}
