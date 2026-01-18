namespace WebAPI_NOVOAssignment.Models;

/// <summary>
/// Represents a refresh token for JWT authentication
/// </summary>
public class RefreshToken
{
    /// <summary>
    /// Unique identifier for the refresh token
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// User ID associated with this token
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The actual refresh token value
    /// </summary>
    public required string Token { get; set; }

    /// <summary>
    /// Expiration date of the refresh token
    /// </summary>
    public DateTime ExpiryDate { get; set; }

    /// <summary>
    /// Indicates if the token is revoked
    /// </summary>
    public bool IsRevoked { get; set; }

    /// <summary>
    /// Date when the token was created
    /// </summary>
    public DateTime CreatedDate { get; set; }
}
