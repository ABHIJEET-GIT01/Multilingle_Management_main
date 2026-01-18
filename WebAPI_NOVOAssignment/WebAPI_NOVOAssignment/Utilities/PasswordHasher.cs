using System.Security.Cryptography;
using System.Text;

namespace WebAPI_NOVOAssignment.Utilities;

/// <summary>
/// Utility class for password hashing and verification
/// </summary>
public static class PasswordHasher
{
    /// <summary>
    /// Hashes a password
    /// </summary>
    /// <param name="password">Plain text password</param>
    /// <returns>Hashed password</returns>
    public static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }

    /// <summary>
    /// Verifies a password against a hash
    /// </summary>
    /// <param name="password">Plain text password</param>
    /// <param name="hash">Password hash</param>
    /// <returns>True if password matches hash, false otherwise</returns>
    public static bool VerifyPassword(string password, string hash)
    {
        var hashOfInput = HashPassword(password);
        return hashOfInput == hash;
    }
}
