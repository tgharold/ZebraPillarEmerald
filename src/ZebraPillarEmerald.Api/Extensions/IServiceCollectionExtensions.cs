using System;
using System.Linq;
using FluentMigrator.Runner;
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
            IConfiguration configuration
            )
        {
            var databaseSection = configuration.GetSection("Database");
            var databaseSettings = databaseSection.Get<DatabaseSettings>();
            services.AddSingleton(typeof(DatabaseSettings), databaseSettings);
            var connectionStringSection = configuration.GetSection("ConnectionStrings");
            var connectionStringSettings = connectionStringSection.Get<ConnectionStringSettings>();
            services.AddSingleton(typeof(ConnectionStringSettings), connectionStringSettings);

            switch (databaseSettings.DatabaseType)
            {
                case DatabaseTypes.PostgreSQL:
                    services.AddDbContext<ZpeDbContext, ZpeDbContextPostGresSql>(
                        x => x.UseNpgsql(connectionStringSettings.ZebraPillarEmerald)
                        );
                    services.AddFluentMigratorCore()
                        .ConfigureRunner(
                            builder => builder
                                .AddPostgres()
                                .WithGlobalConnectionString(connectionStringSettings.ZebraPillarEmerald)
                                .ScanIn(typeof(MigrationMarker).Assembly).For.Migrations())
                        ;
                    break;
                
                case DatabaseTypes.SQLiteMemory:
                    services.AddDbContext<ZpeDbContext, ZpeDbContextSQLiteMemory>(
                        x => ConfigureSQLiteMemory(x, connectionStringSettings)
                        );
                    services.AddFluentMigratorCore()
                        .ConfigureRunner(
                            builder => builder
                                .AddSQLite()
                                .WithGlobalConnectionString(connectionStringSettings.ZebraPillarEmerald)
                                .ScanIn(typeof(MigrationMarker).Assembly).For.Migrations())
                        ;
                    break;
                
                case DatabaseTypes.SQLServer:
                    services.AddDbContext<ZpeDbContext, ZpeDbContextSqlServer>(
                        x => x.UseSqlServer(connectionStringSettings.ZebraPillarEmerald)
                        );
                    services.AddFluentMigratorCore()
                        .ConfigureRunner(
                            builder => builder
                                .AddSqlServer()
                                .WithGlobalConnectionString(connectionStringSettings.ZebraPillarEmerald)
                                .ScanIn(typeof(MigrationMarker).Assembly).For.Migrations())
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

        /// <summary>SQLite in-memory is a database that only exists during the application's lifetime.
        /// It's only to be used in testing environments where data can be discarded at the end of a run.
        /// </summary>
        private static void ConfigureSQLiteMemory(
            DbContextOptionsBuilder options, 
            ConnectionStringSettings connectionStringSettings
            )
        {
            var dbName = DateTimeOffset.UtcNow.Ticks.ToString().PadLeft(10, '0');
            dbName = dbName.Substring(dbName.Length-10);

            if (string.IsNullOrWhiteSpace(connectionStringSettings.ZebraPillarEmerald))
                connectionStringSettings.ZebraPillarEmerald = $"Data Source=file:memZpeTest{dbName}?mode=memory&cache=shared";

            options.UseSqlite(connectionStringSettings.ZebraPillarEmerald);
        }
    }
}