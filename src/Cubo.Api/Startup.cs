using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Cubo.Core.Repositories;
using Cubo.Core.Services;
using Cubo.Core.Mappers;
using Cubo.Api.Middleware;

namespace Cubo.Api
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
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
            services.AddControllers();
            services.AddSingleton<IBucketRepository, InMemoryBucketRepository>();
            services.AddScoped<IBucketService, BucketService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddSingleton<IMapper>(x => AutoMapperConfig.GetMapper());
            services.AddSingleton<IDataInitializer, DataInitializer>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var dataInitializer = app.ApplicationServices.GetService<IDataInitializer>();

            if (dataInitializer != null) dataInitializer.SeedAsync();
        }
    }
}
