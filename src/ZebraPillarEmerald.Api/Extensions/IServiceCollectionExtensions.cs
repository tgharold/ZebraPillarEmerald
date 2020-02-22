using System;
using System.Linq;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZebraPillarEmerald.Core.Database;
using ZebraPillarEmerald.Migrations;

namespace ZebraPillarEmerald.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void ConfigureDatabase(
            this IServiceCollection services,
            IWebHostEnvironment environment,
            DatabaseSettings databaseSettings,
            ConnectionStringSettings connectionStringSettings
            )
        {
          
            switch (databaseSettings.DatabaseType)
            {
                case DatabaseTypes.PostgreSQL:
                    services.AddDbContext<ZpeDbContext, ZpeDbContextPostGresSql>(
                        x => x.UseNpgsql(connectionStringSettings.ZebraPillarEmerald)
                        );
                    services
                        .AddFluentMigratorCore()
                        .ConfigureRunner(
                            builder => builder
                                .AddPostgres()
                                .WithGlobalConnectionString(connectionStringSettings.ZebraPillarEmerald)
                                .ScanIn(typeof(MigrationMarker).Assembly).For.Migrations())
                        .Configure<RunnerOptions>(cfg => cfg.Profile = environment.EnvironmentName)
                        ;
                    break;
                
                case DatabaseTypes.SQLiteMemory:
                    if (string.IsNullOrWhiteSpace(connectionStringSettings.ZebraPillarEmerald))
                    {
                        var dbName = DateTimeOffset.UtcNow.Ticks.ToString().PadLeft(10, '0');
                        dbName = dbName.Substring(dbName.Length-10);
                        connectionStringSettings.ZebraPillarEmerald = $"Data Source=file:memZpeTest{dbName}?mode=memory&cache=shared";
                    }
                    services.AddDbContext<ZpeDbContext, ZpeDbContextSQLiteMemory>(
                        x => x.UseSqlite(connectionStringSettings.ZebraPillarEmerald)
                        );
                    services
                        .AddFluentMigratorCore()
                        .ConfigureRunner(
                            builder => builder
                                .AddSQLite()
                                .WithGlobalConnectionString(connectionStringSettings.ZebraPillarEmerald)
                                .ScanIn(typeof(MigrationMarker).Assembly).For.Migrations())
                        .Configure<RunnerOptions>(cfg => cfg.Profile = environment.EnvironmentName)
                        ;
                    break;
                
                case DatabaseTypes.SQLite:
                    services.AddDbContext<ZpeDbContext, ZpeDbContextSQLite>(
                        x => x.UseSqlite(connectionStringSettings.ZebraPillarEmerald)
                    );
                    services
                        .AddFluentMigratorCore()
                        .ConfigureRunner(
                            builder => builder
                                .AddSQLite()
                                .WithGlobalConnectionString(connectionStringSettings.ZebraPillarEmerald)
                                .ScanIn(typeof(MigrationMarker).Assembly).For.Migrations())
                        .Configure<RunnerOptions>(cfg => cfg.Profile = environment.EnvironmentName)
                        ;
                    break;
                
                case DatabaseTypes.SQLServer:
                    services.AddDbContext<ZpeDbContext, ZpeDbContextSqlServer>(
                        x => x.UseSqlServer(connectionStringSettings.ZebraPillarEmerald)
                        );
                    services
                        .AddFluentMigratorCore()
                        .ConfigureRunner(
                            builder => builder
                                .AddSqlServer()
                                .WithGlobalConnectionString(connectionStringSettings.ZebraPillarEmerald)
                                .ScanIn(typeof(MigrationMarker).Assembly).For.Migrations())
                        .Configure<RunnerOptions>(cfg => cfg.Profile = environment.EnvironmentName)
                        ;
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(databaseSettings.DatabaseType),
                        $"'{databaseSettings.DatabaseType}' is not a supported database type.  " +
                        $"Supported types are: {string.Join(", ", DatabaseTypes.All.Value.Select(x => $"'{x}'"))}."
                        );
            }
        }
    }
}