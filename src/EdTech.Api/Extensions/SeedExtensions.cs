using EdTech.Api.Extensions;
using EdTech.Data;
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
            var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await serviceScope.ServiceProvider.CreateRolesAsync();
            await serviceScope.ServiceProvider.CreateUsersAsync();

            // var mapper = serviceScope.ServiceProvider.GetRequiredService<IMapper>();

            // await new CategoriesSeed(context).LoadAsync();

            return app;
        }
    }
}