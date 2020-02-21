using FluentMigrator;

namespace ZebraPillarEmerald.Migrations.Migrations
{
    [Migration(202002170001)]
    public class InitialMigration : ForwardOnlyMigration
    {
        private const string SchemaName = "zpe";

        public override void Up()
        {
            // IfDatabase("sqlite")...
            // https://fluentmigrator.github.io/articles/multi-db-support.html
            
            Create
                .Table("Group")
                .InSchema(SchemaName)
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                ;
            
            Create
                .Table("Ticket")
                .InSchema(SchemaName)
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                ;
            
            Create
                .Table("Users")
                .InSchema(SchemaName)
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                .WithColumn("Name").AsString(250).NotNullable()
                ;
        }
    }
}