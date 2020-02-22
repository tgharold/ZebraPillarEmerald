using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZebraPillarEmerald.Core.Database;

namespace ZebraPillarEmerald.Api.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void ConfigureDatabaseContext(
            this IServiceCollection services, 
            IConfiguration configuration
            )
        {
            var databaseSection = configuration.GetSection("Database");
            services.Configure<DatabaseSettings>(configuration.GetSection("Database"));
            var databaseSettings = databaseSection.Get<DatabaseSettings>();

            switch (databaseSettings.DatabaseType)
            {
                case "PostgreSQL":
                    services.AddDbContext<ZpeDbContext, ZpeDbContextPostGresSql>(
                        x => ConfigurePostgresSQL(x, databaseSettings)
                        );
                    break;
                
                case "SQLiteMemory":
                    services.AddDbContext<ZpeDbContext, ZpeDbContextSQLiteMemory>(
                        x => ConfigureSQLiteMemory(x, databaseSettings)
                        );
                    break;
                
                case "SQLServer":
                    services.AddDbContext<ZpeDbContext, ZpeDbContextSqlServer>(
                        x => ConfigureSQLServer(x, databaseSettings)
                        );
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(databaseSettings.DatabaseType),
                        $"'{databaseSettings.DatabaseType}' is not a supported database type."
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
                ?? "user id=sa;password=YourStrong!Passw0rd;server=localhost,1433;database=PeopleDatabase;Trusted_Connection=no";
            options.UseSqlServer(connString);
        }
        
        private static void ConfigurePostgresSQL(
            DbContextOptionsBuilder options, 
            DatabaseSettings databaseSettings
            )
        {
            string connString = databaseSettings?.ConnectionString 
                ?? "Host=localhost;Database=PeopleDatabase;Username=postgres;Password=example";
            options.UseNpgsql(connString);
        }
    }
}