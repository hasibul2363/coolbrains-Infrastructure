using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoolBrains.Infrastructure.Extensions;
using CoolBrains.Infrastructure.Host.AspNetCore.Authentication;
using CoolBrains.Infrastructure.OAuth;
using CoolBrains.Infrastructure.Session;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

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
            //services.AddTransient<UserContext>();
            //services.AddTransient<RequestInfo>();
            services.AddTransient<AuthenticationService>();
            //services.AddTransient<IOauthAccessTokenGenerator, OauthAccessTokenGenerator>();
            //services.Configure<TokenConfig>(Configuration.GetSection("TokenConfig"));
            //services.AddJwtBearerAuthentication(Configuration);
            services
                .AddAuth(Configuration);
            services.AddControllers();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseWebSecurityContextInitializer();



            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
