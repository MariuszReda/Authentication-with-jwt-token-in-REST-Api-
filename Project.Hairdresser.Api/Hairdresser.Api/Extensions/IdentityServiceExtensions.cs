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

            //services.AddIdentityCore<Account>(
            //    options =>
            //{
            //    options.User.RequireUniqueEmail = true;
            //    options.Password.RequireDigit = true;
            //    options.Password.RequireUppercase = true;
            //    options.Password.RequireNonAlphanumeric = false;
            //}
           // )
                    .AddRoles<Role>()
                    .AddRoleManager<RoleManager<Role>>()
                    .AddSignInManager<SignInManager<Account>>()
                    .AddRoleValidator<RoleValidator<Role>>()
                    .AddEntityFrameworkStores<DataContext>();

            return services;
        }
    }
}
