using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ZebraPillarEmerald.Api.Extensions;
using ZebraPillarEmerald.Core.Database;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ZebraPillarEmerald.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _environment;

        public Startup(
            IConfiguration configuration,
            IWebHostEnvironment environment
            )
        {
            _environment = environment;
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(
            IServiceCollection services
            )
        {
            //TODO: simplify this block of code
            var databaseSettings = new DatabaseSettings();
            _configuration.GetSection("Database").Bind(databaseSettings);
            services.AddSingleton(typeof(DatabaseSettings), databaseSettings);
            var connectionStringSettings = new ConnectionStringSettings();
            _configuration.GetSection("ConnectionStrings").Bind(connectionStringSettings);
            services.AddSingleton(typeof(ConnectionStringSettings), connectionStringSettings);

            services.ConfigureDatabase(_environment, databaseSettings, connectionStringSettings);

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IServiceProvider serviceProvider
            )
        {
            if (_environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.MigrateDatabase(serviceProvider);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
