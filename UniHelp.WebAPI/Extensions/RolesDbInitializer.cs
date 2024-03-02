using Microsoft.AspNetCore.Identity;
using UniHelp.Features.Constants;

namespace UniHelp.WebAPI.Extensions;

public class RolesDbInitializer
{
    public static async Task SeedRolesToDbAsync(IApplicationBuilder builder)
    {
        using var serviceScope = builder.ApplicationServices.CreateScope();

        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        foreach (var roleName in UserRoleNames.Roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var role = new IdentityRole { Name = roleName };

                await roleManager.CreateAsync(role);
            }
        }
    }
}