using Hairdresser.Api.Data;
using Hairdresser.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Hairdresser.Api.Extensions
{ 
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, 
            IConfiguration configuration)
        {
        services.AddIdentity<Account, Role>()
                    .AddRoles<Role>()
                    .AddRoleManager<RoleManager<Role>>()
                    .AddSignInManager<SignInManager<Account>>()
                    .AddRoleValidator<RoleValidator<Role>>()
                    .AddEntityFrameworkStores<DataContext>();

            return services;
        }
    }
}
