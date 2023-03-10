using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TicketApp_API.Data.Configurations;

public static class SeedData
{
    public static async Task Seed(UserManager<IdentityUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        await SeedRoles(roleManager);
        await SeedUsers(userManager);
    }

    private static async Task SeedUsers(UserManager<IdentityUser> userManager)
    {
        if (await userManager.FindByEmailAsync("admin@ticket.pl") == null)
        {
            var user = new IdentityUser
            {
                UserName = "admin@ticket.pl",
                Email = "admin@ticket.pl"
            };
            var result = await userManager.CreateAsync(user, "P@ssword1");
            if (result.Succeeded) await userManager.AddToRoleAsync(user, "Administrator");
        }

        if (await userManager.FindByEmailAsync("customer1@gmail.com") == null)
        {
            var user = new IdentityUser
            {
                UserName = "customer1@gmail.com",
                Email = "customer1@gmail.com"
            };
            var result = await userManager.CreateAsync(user, "P@ssword1");
            if (result.Succeeded) await userManager.AddToRoleAsync(user, "Customer");
        }

        if (await userManager.FindByEmailAsync("customer2@gmail.com") == null)
        {
            var user = new IdentityUser
            {
                UserName = "customer2@gmail.com",
                Email = "customer2@gmail.com"
            };
            var result = await userManager.CreateAsync(user, "P@ssword1");
            if (result.Succeeded) await userManager.AddToRoleAsync(user, "Customer");
        }
    }

    private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("Administrator"))
        {
            var role = new IdentityRole
            {
                Name = "Administrator"
            };
            await roleManager.CreateAsync(role);
        }

        if (!await roleManager.RoleExistsAsync("Customer"))
        {
            var role = new IdentityRole
            {
                Name = "Customer"
            };
            await roleManager.CreateAsync(role);
        }
    }
}