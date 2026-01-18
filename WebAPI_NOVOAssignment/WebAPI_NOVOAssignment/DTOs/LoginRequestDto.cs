namespace WebAPI_NOVOAssignment.DTOs;

/// <summary>
/// Request DTO for user login
/// </summary>
public class LoginRequestDto
{
    /// <summary>
    /// User's email address
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// User's password
    /// </summary>
    public required string Password { get; set; }
}
