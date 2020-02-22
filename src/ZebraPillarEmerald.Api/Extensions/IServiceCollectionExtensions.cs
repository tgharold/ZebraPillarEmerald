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
            services.Configure<DatabaseSettings>(configuration.GetSection("Database"));
            var databaseSettings = databaseSection.Get<DatabaseSettings>();

            switch (databaseSettings.DatabaseType)
            {
                case DatabaseTypes.PostgreSQL:
                    services.AddDbContext<ZpeDbContext, ZpeDbContextPostGresSql>(
                        x => ConfigurePostgresSQL(x, databaseSettings)
                        );
                    services.AddFluentMigratorCore()
                        .ConfigureRunner(
                            builder => builder
                                .AddPostgres()
                                .WithGlobalConnectionString(databaseSettings.ConnectionString)
                                .ScanIn(typeof(MigrationMarker).Assembly).For.Migrations())
                        ;
                    break;
                
                case DatabaseTypes.SQLiteMemory:
                    services.AddDbContext<ZpeDbContext, ZpeDbContextSQLiteMemory>(
                        x => ConfigureSQLiteMemory(x, databaseSettings)
                        );
                    services.AddFluentMigratorCore()
                        .ConfigureRunner(
                            builder => builder
                                .AddSQLite()
                                .WithGlobalConnectionString(databaseSettings.ConnectionString)
                                .ScanIn(typeof(MigrationMarker).Assembly).For.Migrations())
                        ;
                    break;
                
                case DatabaseTypes.SQLServer:
                    services.AddDbContext<ZpeDbContext, ZpeDbContextSqlServer>(
                        x => ConfigureSQLServer(x, databaseSettings)
                        );
                    services.AddFluentMigratorCore()
                        .ConfigureRunner(
                            builder => builder
                                .AddSqlServer()
                                .WithGlobalConnectionString(databaseSettings.ConnectionString)
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

        private static void ConfigureSQLiteMemory(
            DbContextOptionsBuilder options, 
            DatabaseSettings databaseSettings
            )
        {
            var dbName = DateTimeOffset.UtcNow.Ticks.ToString().PadLeft(10, '0');
            dbName = dbName.Substring(dbName.Length-10);
            var connString = databaseSettings?.ConnectionString 
                ?? $"Data Source=file:memMigrateTest{dbName}?mode=memory&cache=shared";

            options.UseSqlite(connString);
        }

        private static void ConfigureSQLServer(
            DbContextOptionsBuilder options, 
            DatabaseSettings databaseSettings
            )
        {
            string connString = databaseSettings?.ConnectionString 
                ?? "user id=sa;password=YourStrong!Passw0rd;server=localhost,1433;database=ZebraPillarEmerald;Trusted_Connection=no";
            options.UseSqlServer(connString);
        }
        
        private static void ConfigurePostgresSQL(
            DbContextOptionsBuilder options, 
            DatabaseSettings databaseSettings
            )
        {
            string connString = databaseSettings?.ConnectionString 
                ?? "Host=localhost;Database=ZebraPillarEmerald;Username=postgres;Password=YourStrong!Passw0rd";
            options.UseNpgsql(connString);
        }
    }
}