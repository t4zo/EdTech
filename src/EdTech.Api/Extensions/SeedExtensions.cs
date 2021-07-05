using EdTech.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace EdTech.Extensions
{
    public static class SeedExtensions
    {
        public static async Task<IApplicationBuilder> SeedDatabaseAsync(this IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.CreateScope();

            await serviceScope.ServiceProvider.CreateRolesAsync();
            await serviceScope.ServiceProvider.CreateUsersAsync();

            return app;
        }
    }
}