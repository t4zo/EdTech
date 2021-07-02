using EdTech.Data;
using EdTech.Options;
using EdTech.Persistence.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using static EdTech.Core.Constants.AuthorizationConstants;

namespace EdTech.Api.Extensions
{
    public static class UsersExtensions
    {
        public static async Task<IServiceProvider> CreateUsersAsync(this IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var appOptions = serviceProvider.GetRequiredService<IOptionsSnapshot<AppOptions>>().Value;

            if (!await context.Users.AnyAsync())
            {
                foreach (var user in appOptions.Users)
                {
                    var applicationUser = new ApplicationUser { Email = user.Email, UserName = user.UserName };
                    var result = await userManager.CreateAsync(applicationUser, user.Password);

                    if (result.Succeeded)
                    {
                        foreach (var role in user.Roles)
                        {
                            await userManager.AddToRoleAsync(applicationUser, role);
                            await SeedUserClaims(userManager, applicationUser, role);
                            await SeedRoleClaims(roleManager, role);
                        }
                    }

                    await context.SaveChangesAsync();
                }
            }

            return serviceProvider;
        }

        private static async Task SeedUserClaims(UserManager<ApplicationUser> userManager, ApplicationUser user, string roleName)
        {
            if (roleName.Equals(Roles.Superuser))
            {
                await userManager.AddClaimsAsync(user, new List<Claim>
                    {
                        new(CustomClaimTypes.Permission, Permissions.Alunos.View),
                        new(CustomClaimTypes.Permission, Permissions.Alunos.Create),
                        new(CustomClaimTypes.Permission, Permissions.Alunos.Update),
                        new(CustomClaimTypes.Permission, Permissions.Alunos.Delete),
                    });
            }
        }

        private static async Task SeedRoleClaims(RoleManager<ApplicationRole> roleManager, string roleName)
        {
            var role = await roleManager.FindByNameAsync(roleName);

            if (roleName.Equals(Roles.Superuser))
            {
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Alunos.View));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Alunos.Create));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Alunos.Update));
                await roleManager.AddClaimAsync(role, new Claim(CustomClaimTypes.Permission, Permissions.Alunos.Delete));
            }
        }
    }
}
