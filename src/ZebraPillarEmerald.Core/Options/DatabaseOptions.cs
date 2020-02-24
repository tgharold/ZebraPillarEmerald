using ZebraPillarEmerald.Core.Attributes;

namespace ZebraPillarEmerald.Core.Options
{
    [ConfigurationSectionName("Database")]
    public class DatabaseOptions
    {
        public DatabaseSchemaNames SchemaNames { get; set; } = new DatabaseSchemaNames(); 
        public string DatabaseType { get; set; }
        
        public class DatabaseSchemaNames
        {
            public string ZebraPillarEmerald { get; set; } = "zpe";
        }
    }
}