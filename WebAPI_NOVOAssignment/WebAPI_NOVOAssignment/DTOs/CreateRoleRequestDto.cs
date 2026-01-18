namespace WebAPI_NOVOAssignment.DTOs;

/// <summary>
/// Request DTO for creating a new role
/// </summary>
public class CreateRoleRequestDto
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}
