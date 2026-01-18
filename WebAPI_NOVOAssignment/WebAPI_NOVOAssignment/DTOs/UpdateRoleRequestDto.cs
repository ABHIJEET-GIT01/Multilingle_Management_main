namespace WebAPI_NOVOAssignment.DTOs;

/// <summary>
/// Request DTO for updating an existing role
/// </summary>
public class UpdateRoleRequestDto
{
    /// <summary>
    /// Role name - 3-50 characters, must be unique
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Role description - maximum 500 characters
    /// </summary>
    public string? Description { get; set; }
}
