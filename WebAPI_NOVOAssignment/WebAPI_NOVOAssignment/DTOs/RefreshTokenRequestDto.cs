namespace WebAPI_NOVOAssignment.DTOs;

/// <summary>
/// Request DTO for refresh token endpoint
/// </summary>
public class RefreshTokenRequestDto
{
    /// <summary>
    /// The refresh token value obtained from login
    /// </summary>
    public required string RefreshToken { get; set; }
}
