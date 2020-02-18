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
                .Table("Users")
                .InSchema(SchemaName)
                .WithColumn("Id").AsInt64().Identity().NotNullable()
                ;
        }
    }
}