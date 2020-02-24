using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using ZebraPillarEmerald.Api.Extensions;
using ZebraPillarEmerald.Core.Database;
using ZebraPillarEmerald.Core.Options;
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
            services.AddOptions();
            var databaseOptions = services.ConfigureAndValidateSection<DatabaseOptions>(_configuration);
            var connectionStringsOptions = services.ConfigureAndValidateSection<ConnectionStringsOptions>(_configuration);

            services.ConfigureDatabase(_environment, databaseOptions, connectionStringsOptions);

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
