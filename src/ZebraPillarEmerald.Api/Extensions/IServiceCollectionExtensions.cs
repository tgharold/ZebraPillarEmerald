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
                    services.AddDbContext<ZpeDbContext, ZpeDbContextPostGresSql>(ConfigurePostgresSQL);
                    break;
                
                case "SQLiteMemory":
                    services.AddDbContext<ZpeDbContext, ZpeDbContextSQLiteMemory>(ConfigureSQLiteMemory);
                    break;
                
                case "SQLServer":
                    services.AddDbContext<ZpeDbContext, ZpeDbContextSqlServer>(ConfigureSQLServer);
                    break;
                
                default:
                    throw new NotImplementedException();
            }
        }

        private static void ConfigureSQLiteMemory(DbContextOptionsBuilder options)
        {
            var dbName = DateTimeOffset.UtcNow.Ticks.ToString().PadLeft(10, '0');
            dbName = dbName.Substring(dbName.Length-10);
            var connString = $"Data Source=file:memMigrateTest{dbName}?mode=memory&cache=shared";

            options.UseSqlite(connString);
        }

        private static void ConfigureSQLServer(DbContextOptionsBuilder options)
        {
            string connString = "user id=sa;password=YourStrong!Passw0rd;server=localhost,1433;database=PeopleDatabase;Trusted_Connection=no";
            options.UseSqlServer(connString);
        }
        
        private static void ConfigurePostgresSQL(DbContextOptionsBuilder options)
        {
            string connString = "Host=localhost;Database=PeopleDatabase;Username=postgres;Password=example";
            options.UseNpgsql(connString);
        }
    }
}