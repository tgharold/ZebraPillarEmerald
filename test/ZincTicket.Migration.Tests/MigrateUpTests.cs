using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using ZincTicket.Migrations.Migrations;
using FluentMigrator.Runner;

namespace ZincTicket.MigrationTests
{
    public class MigrateUpTests
    {
        private ServiceProvider CreateServiceProvider(string connectionString)
        {
            return new ServiceCollection()
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .AddFluentMigratorCore()
                .ConfigureRunner(
                    builder => builder
                        .AddSQLite()
                        .WithGlobalConnectionString(connectionString)
                        .WithMigrationsIn(typeof(InitialMigration).Assembly))
                .BuildServiceProvider();
        }        
        
        [Fact]
        public void Can_migrate_up_SQLite()
        {
            var dbName = DateTimeOffset.UtcNow.Ticks.ToString().PadLeft(10, '0');
            dbName = dbName.Substring(dbName.Length-10);
            var connString = $"Data Source=file:memMigrateTest{dbName}?mode=memory&cache=shared";
            
            var serviceProvider = this.CreateServiceProvider(connString);

            using (var scope = serviceProvider.CreateScope())
            {
                var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                runner.ListMigrations();
                runner.MigrateUp();

                string sqlStatement = "SELECT Description FROM VersionInfo";

                var dataSet = runner.Processor.Read(sqlStatement, string.Empty);

                Assert.NotNull(dataSet);
                Assert.Equal(nameof(InitialMigration), dataSet.Tables[0].Rows[0].ItemArray[0]);
            }
        }
    }
}