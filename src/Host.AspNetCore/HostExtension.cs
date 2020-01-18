using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Host.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace CoolBrains.Infrastructure.Host.AspNetCore
{
    public static class HostExtension
    {
        public static void UseCoolBrains(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseWebSecurityContextInitializer();

            var origins = configuration.GetValue<string>("AllowedOrigins").Split(',');
            app.UseCors(builder => builder
                .WithOrigins(origins)
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }



    }
}
