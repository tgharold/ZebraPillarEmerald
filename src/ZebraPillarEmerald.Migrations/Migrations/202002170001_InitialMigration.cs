using FluentMigrator;
using Microsoft.Extensions.Options;
using ZebraPillarEmerald.Core.Options;

namespace ZebraPillarEmerald.Migrations.Migrations
{
    [Migration(202002170001)]
    public class InitialMigration : ForwardOnlyMigration
    {
        private readonly DatabaseOptions _databaseOptionsOptions;

        public InitialMigration(
            IOptions<DatabaseOptions> optionsAccessor
            )
        {
            _databaseOptionsOptions = optionsAccessor.Value;
        }
        
        public override void Up()
        {
            var schemaName = _databaseOptionsOptions.SchemaNames.ZebraPillarEmerald;

            // IfDatabase("sqlite")...
            // https://fluentmigrator.github.io/articles/multi-db-support.html
            
            Create
                .Table("Group")
                .InSchema(schemaName)
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                ;
            
            Create
                .Table("Ticket")
                .InSchema(schemaName)
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                ;
            
            Create
                .Table("Users")
                .InSchema(schemaName)
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                ;
        }
    }
}