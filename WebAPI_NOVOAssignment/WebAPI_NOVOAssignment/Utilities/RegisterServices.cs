using Microsoft.EntityFrameworkCore.Storage;
using WebAPI_NOVOAssignment.Repositories;
using WebAPI_NOVOAssignment.Repositories.Interfaces;
using WebAPI_NOVOAssignment.Services;
using WebAPI_NOVOAssignment.Services.Interfaces;
using WebAPI_NOVOAssignment.Utilities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebAPI_NOVOAssignment.WebAPI_NOVOAssignment.Utilities
{
    public class RegisterServices
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // Register Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Register Services
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<ILocalizationService, LocalizationService>();
            services.AddScoped<JwtTokenGenerator>();
        }
    }
}
