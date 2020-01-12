using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Host.AspNetCore.Authentication;
using CoolBrains.Infrastructure.OAuth;
using CoolBrains.Infrastructure.Session;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
namespace OAuthTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<UserContext>();
            services.AddTransient<RequestInfo>();
            services.AddTransient<AuthenticationService>();
            services.AddTransient<IOauthAccessTokenGenerator, OauthAccessTokenGenerator>();
            services.Configure<TokenConfig>(Configuration.GetSection("TokenConfig"));
            services.AddControllers();
            services.AddJwtBearerAuthentication(Configuration);
            services.AddMvc(a => a.EnableEndpointRouting = false);
            //MvcOptions.EnableEndpointRouting = false;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.UseMvc();

            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}


            //app.UseRouting();

            //app.UseAuthorization();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});



            //app.UseAuthentication();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        template: "[controller]/[action]");
            //});
        }
    }
}
