namespace WebAPI_NOVOAssignment.DTOs;

/// <summary>
/// Request DTO for updating an existing user
/// </summary>
public class UpdateUserRequestDto
{
    public required string Username { get; set; }

    public required string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public List<Guid> RoleIds { get; set; } = [];
}
