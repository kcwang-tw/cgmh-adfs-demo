using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PrimaryApi.Core.Interfaces;
using PrimaryApi.DataAccess.FromTestApi.Applications;
using PrimaryApi.WebApi.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimaryApi.WebApi
{
    public class StartupDevelopment
    {
        public StartupDevelopment(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddHttpClient("test-api", c => c.BaseAddress = new Uri("https://localhost:44382/api/"));
            services.AddCors();


            services.AddAuthentication(x =>
            {
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.Authority = "https://adfs2016.southeastasia.cloudapp.azure.com/adfs";
                options.Audience = @"https://localhost:44326";
                //options.Audience = "microsoft:identityserver:3dfb9ea1-4255-4156-91c5-a79fab842408";

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = "http://adfs2016.southeastasia.cloudapp.azure.com/adfs/services/trust"
                };
            });

            //services
            //    .AddAuthorization(options =>
            //    {
            //        options.DefaultPolicy = new AuthorizationPolicyBuilder()
            //            .RequireAuthenticatedUser()
            //            .AddAuthenticationSchemes("ADFS")
            //            .Build();
            //    });

            // AutoMapper
            var mappingConfig = new MapperConfiguration(c =>
            {
                c.AddProfile(new AutoMapperProfiles());
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Injection
            services.AddScoped<IRanksQuery, RanksTestApi>();
            services.AddScoped<ISeatsQuery, SeatsTestApi>();
            services.AddScoped<ISeatsCommand, SeatsTestApi>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
