using EdTech.Core;
using EdTech.Data;
using EdTech.Extensions;
using EdTech.i18n;
using EdTech.Options;
using EdTech.Persistence.Identity;
using EdTech.Persistence.Repositories;
using FluentValidation.AspNetCore;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace EdTech
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAlunosRepository, AlunosRepository>();

            services.AddCustomCors();

            services.AddDbContext<ApplicationDbContext>();

            services.AddOptions<AppOptions>().Bind(Configuration.GetSection(nameof(AppOptions)));

            services.AddProblemDetails(configure => { configure.IncludeExceptionDetails = (ctx, exp) => true; });

            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddSwagger();

            services.AddAutoMapper(typeof(Startup));

            services.AddFluentValidation(configureExpression =>
            {
                configureExpression.LocalizationEnabled = true;
                configureExpression.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequireDigit = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;

                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 10;
                    options.Lockout.AllowedForNewUsers = false;

                    options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    options.User.RequireUniqueEmail = true;
                })
                .AddSignInManager()
                .AddErrorDescriber<PortugueseIdentityErrorDescriber>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseConfiguredSwagger();
            }

            app.SeedDatabaseAsync().GetAwaiter().GetResult();

            app.UseHttpsRedirection();
            app.UseProblemDetails();

            app.UseRouting();
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
