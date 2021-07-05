using EdTech.Core.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EdTech.Extensions
{
    public static class CorsExtensions
    {
        public static IServiceCollection AddCustomCors(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var allowedOrigins = configuration.GetSection(AuthorizationConstants.AllowedOrigins).Get<string[]>();
            allowedOrigins ??= Array.Empty<string>();

            services.AddCors(setupAction =>
            {
                setupAction.AddDefaultPolicy(configurePolicy =>
                {
                    configurePolicy
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            return services;
        }
    }
}