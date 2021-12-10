using Microsoft.AspNetCore.Identity;
using restaurant_system.Models;
using System.Threading.Tasks;
namespace restaurant_system
{
    public class RoleInitializer
    {
        public static async Task InitializeAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string managerEmail = "manager@gmail.com";
            string waiterEmail = "waiter@gmail.com";
            string cookEmail = "cook@gmail.com";
            string password = "123";

            if (await roleManager.FindByNameAsync(UserRoles.Admin) == null)
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (await roleManager.FindByNameAsync(UserRoles.Manager) == null)
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
            if (await roleManager.FindByNameAsync(UserRoles.Waiter) == null)
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Waiter));
            if (await roleManager.FindByNameAsync(UserRoles.Cook) == null)
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Cook));

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                User user = new User { Email = adminEmail, UserName = adminEmail };
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserRoles.Admin);
                    await userManager.AddToRoleAsync(user, UserRoles.Cook);
                    await userManager.AddToRoleAsync(user, UserRoles.Waiter);
                    await userManager.AddToRoleAsync(user, UserRoles.Manager);

                }
            }

            if (await userManager.FindByNameAsync(managerEmail) == null)
            {
                User user = new User { Email = managerEmail, UserName = managerEmail };
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserRoles.Cook);
                    await userManager.AddToRoleAsync(user, UserRoles.Waiter);
                    await userManager.AddToRoleAsync(user, UserRoles.Manager);

                }
            }

            if (await userManager.FindByNameAsync(waiterEmail) == null)
            {
                User user = new User { Email = waiterEmail, UserName = waiterEmail };
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserRoles.Waiter);

                }
            }

            if (await userManager.FindByNameAsync(cookEmail) == null)
            {
                User user = new User { Email = cookEmail, UserName = cookEmail };
                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserRoles.Cook);

                }
            }

        }

    }
}

