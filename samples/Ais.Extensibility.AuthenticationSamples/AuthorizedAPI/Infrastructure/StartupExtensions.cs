using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace AuthorizedAPI.Infrastructure
{
    internal static class StartupExtensions
    {
        public static IServiceCollection AddAppAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    configuration.Bind("JwtBearer", options);
                });
            return services;
        }
    }
}
