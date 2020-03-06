using System;
using FluentMigrator;
using Microsoft.Extensions.Options;
using ZebraPillarEmerald.Core.Settings;

namespace ZebraPillarEmerald.Migrations.Migrations
{
    [Migration(202002170001)]
    public class InitialMigration : ForwardOnlyMigration
    {
        private readonly DatabaseSettings _databaseSettingsSettings;

        public InitialMigration(
            IOptions<DatabaseSettings> optionsAccessor
            )
        {
            _databaseSettingsSettings = optionsAccessor.Value;
        }
        
        public override void Up()
        {
            var zpeSchemaName = _databaseSettingsSettings.SchemaNames.ZebraPillarEmerald;
            
            //TODO: The following can be removed at some point after we add validation around IOptions<T>
            if (string.IsNullOrEmpty(zpeSchemaName))
                throw new ArgumentNullException(
                    $"{nameof(DatabaseSettings.SchemaNames)}.{nameof(DatabaseSettings.DatabaseSchemaNames.ZebraPillarEmerald)} is missing.");

            // IfDatabase("sqlite")...
            // https://fluentmigrator.github.io/articles/multi-db-support.html
            
            Create
                .Table("Group")
                .InSchema(zpeSchemaName)
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                ;
            
            Create
                .Table("Ticket")
                .InSchema(zpeSchemaName)
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                ;
            
            Create
                .Table("Users")
                .InSchema(zpeSchemaName)
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                ;
        }
    }
}