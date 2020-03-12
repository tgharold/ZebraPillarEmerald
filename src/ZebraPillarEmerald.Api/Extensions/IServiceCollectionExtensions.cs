using System;
using System.Linq;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ZebraPillarEmerald.Core.Database;
using ZebraPillarEmerald.Migrations;

namespace ZebraPillarEmerald.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void ConfigureDatabase(
            this IServiceCollection services,
            IWebHostEnvironment environment
            )
        {
            services.AddScoped<IOptions<RunnerOptions>>()
                .Configure<RunnerOptions>(cfg =>
                {
                    cfg.Profile = environment.EnvironmentName;
                })
                .AddScoped<ZebraPillarEmeraldConnectionStringReader>()
                .AddFluentMigratorCore();

            switch (databaseSettingsSettings.DatabaseType)
            {
                case DatabaseTypes.PostgreSQL:
                    services.AddDbContext<ZpeDbContext, ZpeDbContextPostGresSql>(
                        x => x.UseNpgsql(connectionStringsSettings.ZebraPillarEmerald)
                        );
                    services
                        .ConfigureRunner(
                            builder => builder
                                .AddPostgres()
                                .WithGlobalConnectionString(connectionStringsSettings.ZebraPillarEmerald)
                                .ScanIn(typeof(MigrationMarker).Assembly).For.Migrations())
                        ;
                    break;
                
                case DatabaseTypes.SQLite:
                    services.AddDbContext<ZpeDbContext, ZpeDbContextSQLite>(
                        x => x.UseSqlite(connectionStringsSettings.ZebraPillarEmerald)
                    );
                    services
                        .ConfigureRunner(
                            builder => builder
                                .AddSQLite()
                                .ScanIn(typeof(MigrationMarker).Assembly).For.Migrations())
                        ;
                    break;
                
                case DatabaseTypes.SQLServer:
                    services.AddDbContext<ZpeDbContext, ZpeDbContextSqlServer>(
                        x => x.UseSqlServer(connectionStringsSettings.ZebraPillarEmerald)
                        );
                    services
                        .ConfigureRunner(
                            builder => builder
                                .AddSqlServer()
                                .ScanIn(typeof(MigrationMarker).Assembly).For.Migrations())
                        ;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(databaseSettingsSettings.DatabaseType),
                        $"'{databaseSettingsSettings.DatabaseType}' is not a supported database type.  " +
                        $"Supported types are: {string.Join(", ", DatabaseTypes.All.Value.Select(x => $"'{x}'"))}."
                        );
            }
        }
    }
}