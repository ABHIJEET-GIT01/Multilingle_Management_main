namespace WebAPI_NOVOAssignment.Models;

/// <summary>
/// Represents a role entity in the system
/// </summary>
public class Role
{
    /// <summary>
    /// Unique identifier for the role
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Role name - must be unique
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Description of the role's purpose and permissions
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Date when role was created
    /// </summary>
    public DateTime CreatedDate { get; set; }

    /// <summary>
    /// Collection of users assigned to this role
    /// </summary>
    public ICollection<User> Users { get; set; } = [];
}
