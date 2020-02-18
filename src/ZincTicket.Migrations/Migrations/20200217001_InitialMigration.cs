using FluentMigrator;

namespace ZincTicket.Migrations.Migrations
{
    [Migration(20200217001)]
    public class InitialMigration : ForwardOnlyMigration
    {
        private const string SchemaName = "zinc";

        public override void Up()
        {
            // IfDatabase("sqlite")...
            // https://fluentmigrator.github.io/articles/multi-db-support.html
            
            Create
                .Table("Group")
                .InSchema(SchemaName)
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                ;
            
            Create
                .Table("Ticket")
                .InSchema(SchemaName)
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                ;
            
            Create
                .Table("Users")
                .InSchema(SchemaName)
                .WithColumn("Id").AsInt32().PrimaryKey().Identity().NotNullable()
                ;
        }
    }
}