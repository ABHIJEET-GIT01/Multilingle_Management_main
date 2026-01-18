namespace WebAPI_NOVOAssignment.DTOs;

/// <summary>
/// Response DTO for user details
/// </summary>
public class UserDetailDto
{
    /// <summary>
    /// User's unique identifier
    /// </summary>
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<string> Roles { get; set; } = [];
}
