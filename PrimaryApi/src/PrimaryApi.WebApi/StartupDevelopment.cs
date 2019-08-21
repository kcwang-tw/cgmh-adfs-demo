using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            app.UseMvc();
        }
    }
}
