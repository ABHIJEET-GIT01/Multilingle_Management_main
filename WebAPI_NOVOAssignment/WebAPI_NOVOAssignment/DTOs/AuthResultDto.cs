namespace WebAPI_NOVOAssignment.DTOs;

/// <summary>
/// Response DTO for authentication containing JWT tokens
/// </summary>
public class AuthResultDto
{
    /// <summary>
    /// JWT access token
    /// </summary>
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
    public DateTime ExpiresAt { get; set; }
    public required List<string> Roles { get; set; }
}
