using Hairdresser.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Hairdresser.Api.Data
{
    public class SeedData
    {
        public static async Task InitializeAsync(UserManager<Account> userManager, RoleManager<Role> roleManager)
        {
            string adminRole = "Admin";
            if (await roleManager.FindByNameAsync(adminRole) == null)
            {
                var role = new Role { Name = adminRole };
                await roleManager.CreateAsync(role);
            }

            string userRole = "User";
            if (await roleManager.FindByNameAsync(userRole) == null)
            {
                var role = new Role { Name = userRole };
                await roleManager.CreateAsync(role);
            }
        }
    }
}
