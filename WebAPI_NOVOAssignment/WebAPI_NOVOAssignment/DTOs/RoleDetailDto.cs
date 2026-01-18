namespace WebAPI_NOVOAssignment.DTOs;

/// <summary>
/// Response DTO for role details
/// </summary>
public class RoleDetailDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
}
