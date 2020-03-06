using System;
using System.Linq;
using System.Reflection;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZebraPillarEmerald.Core.Attributes;
using ZebraPillarEmerald.Core.Database;
using ZebraPillarEmerald.Core.Extensions;
using ZebraPillarEmerald.Core.Settings;
using ZebraPillarEmerald.Migrations;

namespace ZebraPillarEmerald.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void ConfigureDatabase(
            this IServiceCollection services,
            IWebHostEnvironment environment,
            DatabaseSettings databaseSettingsSettings,
            ConnectionStringsSettings connectionStringsSettings
            )
        {
          
            switch (databaseSettingsSettings.DatabaseType)
            {
                case DatabaseTypes.PostgreSQL:
                    services.AddDbContext<ZpeDbContext, ZpeDbContextPostGresSql>(
                        x => x.UseNpgsql(connectionStringsSettings.ZebraPillarEmerald)
                        );
                    services
                        .AddFluentMigratorCore()
                        .ConfigureRunner(
                            builder => builder
                                .AddPostgres()
                                .WithGlobalConnectionString(connectionStringsSettings.ZebraPillarEmerald)
                                .ScanIn(typeof(MigrationMarker).Assembly).For.Migrations())
                        .Configure<RunnerOptions>(cfg => cfg.Profile = environment.EnvironmentName)
                        ;
                    break;
                
                case DatabaseTypes.SQLiteMemory:
                    if (string.IsNullOrWhiteSpace(connectionStringsSettings.ZebraPillarEmerald))
                    {
                        var dbName = DateTimeOffset.UtcNow.Ticks.ToString().PadLeft(10, '0');
                        dbName = dbName.Substring(dbName.Length-10);
                        connectionStringsSettings.ZebraPillarEmerald = $"Data Source=file:memZpeTest{dbName}?mode=memory&cache=shared";
                    }
                    services.AddDbContext<ZpeDbContext, ZpeDbContextSQLiteMemory>(
                        x => x.UseSqlite(connectionStringsSettings.ZebraPillarEmerald)
                        );
                    services
                        .AddFluentMigratorCore()
                        .ConfigureRunner(
                            builder => builder
                                .AddSQLite()
                                .WithGlobalConnectionString(connectionStringsSettings.ZebraPillarEmerald)
                                .ScanIn(typeof(MigrationMarker).Assembly).For.Migrations())
                        .Configure<RunnerOptions>(cfg => cfg.Profile = environment.EnvironmentName)
                        ;
                    break;
                
                case DatabaseTypes.SQLite:
                    services.AddDbContext<ZpeDbContext, ZpeDbContextSQLite>(
                        x => x.UseSqlite(connectionStringsSettings.ZebraPillarEmerald)
                    );
                    services
                        .AddFluentMigratorCore()
                        .ConfigureRunner(
                            builder => builder
                                .AddSQLite()
                                .WithGlobalConnectionString(connectionStringsSettings.ZebraPillarEmerald)
                                .ScanIn(typeof(MigrationMarker).Assembly).For.Migrations())
                        .Configure<RunnerOptions>(cfg => cfg.Profile = environment.EnvironmentName)
                        ;
                    break;
                
                case DatabaseTypes.SQLServer:
                    services.AddDbContext<ZpeDbContext, ZpeDbContextSqlServer>(
                        x => x.UseSqlServer(connectionStringsSettings.ZebraPillarEmerald)
                        );
                    services
                        .AddFluentMigratorCore()
                        .ConfigureRunner(
                            builder => builder
                                .AddSqlServer()
                                .WithGlobalConnectionString(connectionStringsSettings.ZebraPillarEmerald)
                                .ScanIn(typeof(MigrationMarker).Assembly).For.Migrations())
                        .Configure<RunnerOptions>(cfg => cfg.Profile = environment.EnvironmentName)
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
        
        public static T ConfigureAndValidateSection<T>(
            this IServiceCollection services,
            IConfiguration configuration
            ) where T : class
        {
            var sectionName = typeof(T).GetCustomAttribute<ConfigurationSectionNameAttribute>()?.SectionName
                              ?? throw new ArgumentNullException(nameof(ConfigurationSectionNameAttribute));
            
            var configurationSection = configuration.GetSection(sectionName);
            
            services.AddOptions<T>()
                .Bind(configurationSection)
                .RecursivelyValidateDataAnnotations()
                ;
            
            return configurationSection.Get<T>();
        }

    }
}