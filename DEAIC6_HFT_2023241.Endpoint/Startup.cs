using DEAIC6_HFT_2023241.Logic;
using DEAIC6_HFT_2023241.Models;
using DEAIC6_HFT_2023241.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;

namespace DEAIC6_HFT_2023241.Endpoint
{
    public class Startup
    {
        //<PackageReference Include = "Microsoft.OpenApi" Version="1.2.3" />
        //<PackageReference Include = "Swashbuckle.AspNetCore.Swagger" Version="5.6.3" />
        //<PackageReference Include = "Swashbuckle.AspNetCore.SwaggerGen" Version="5.6.3" />
        //<PackageReference Include = "Swashbuckle.AspNetCore.SwaggerGenUI" Version="5.6.3" />
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<RoverDbContext>();

            services.AddTransient<IRepository<Rover>, RoverRepository>();
            services.AddTransient<IRepository<RoverBuilder>, RoverBuilderRepository>();
            services.AddTransient<IRepository<VisitedPlaces>, VisitedPlacesRepository>();

            services.AddTransient<IRoverLogic, RoverLogic>();
            services.AddTransient<IRoverBuilderLogic, RoverBuilderLogic>();
            services.AddTransient<IVisitedPlacesLogic, VisitedPlacesLogic>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DEAIC6_HFT_2023241.Endpoint", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DEAIC6_HFT_2023241_API");
                c.RoutePrefix = string.Empty;
                //swagger
            });

            app.UseExceptionHandler(c => c.Run(async context =>
            {
                var ex = context.Features
                    .Get<IExceptionHandlerPathFeature>()
                    .Error;
                var resp = new { Msg = ex.Message };
                await context.Response.WriteAsJsonAsync(resp);
            }));

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
