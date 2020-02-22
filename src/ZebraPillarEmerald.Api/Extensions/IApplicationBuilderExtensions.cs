using System;
using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ZebraPillarEmerald.Api.Extensions
{
    public static class IApplicationBuilderExtensions
    {
        public static void MigrateDatabase(
            this IApplicationBuilder applicationBuilder,
            IServiceProvider serviceProvider
            )
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.MigrateUp();
            }      
        }
    }
}