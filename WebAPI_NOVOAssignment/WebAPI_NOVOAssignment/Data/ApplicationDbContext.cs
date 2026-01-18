using Microsoft.EntityFrameworkCore;
using WebAPI_NOVOAssignment.Models;
using WebAPI_NOVOAssignment.Utilities;

namespace WebAPI_NOVOAssignment.Data;

public class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the ApplicationDbContext
    /// </summary>
    /// <param name="options">DbContext options</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.ConfigureWarnings(w => 
            w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    /// <summary>
    /// Configures the database schema
    /// </summary>
    /// <param name="modelBuilder">The model builder</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(256);
            entity.Property(e => e.PasswordHash).IsRequired();
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.HasIndex(e => e.Username).IsUnique();
            entity.HasIndex(e => e.Email).IsUnique();

            // Relationships
            entity.HasMany(e => e.Roles)
                .WithMany(r => r.Users)
                .UsingEntity("UserRoles");
        });

        // Role configuration
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.HasIndex(e => e.Name).IsUnique();
        });

        // RefreshToken configuration
        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Token).IsRequired();
            entity.Property(e => e.IsRevoked).HasDefaultValue(false);
        });

        // default roles
        var adminRoleId = Guid.NewGuid();
        var userRoleId = Guid.NewGuid();

        modelBuilder.Entity<Role>().HasData(
            new Role
            {
                Id = adminRoleId,
                Name = "Admin",
                Description = "Administrator with full system access",
                CreatedDate = DateTime.UtcNow
            },
            new Role
            {
                Id = userRoleId,
                Name = "User",
                Description = "Regular user with limited permissions",
                CreatedDate = DateTime.UtcNow
            }
        );

        // default admin user
        var adminUserId = Guid.NewGuid();
        var adminPassword = "Admin@123456"; // Default admin password
        var hashedPassword = PasswordHasher.HashPassword(adminPassword);
        var utcNow = DateTime.UtcNow;

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = adminUserId,
                Username = "admin",
                Email = "admin@novo.com",
                PasswordHash = hashedPassword,
                FirstName = "System",
                LastName = "Administrator",
                CreatedDate = utcNow,
                UpdatedDate = utcNow
            }
        );

        // Assign Admin role to admin user
        modelBuilder.Entity("UserRoles").HasData(
     new { UsersId = adminUserId, RolesId = adminRoleId }
 );
    }
}
