using System.ComponentModel.DataAnnotations;
using ZebraPillarEmerald.Core.Attributes;

namespace ZebraPillarEmerald.Core.Settings
{
    [ConfigurationSectionName("Database")]
    public class DatabaseSettings
    {
        public DatabaseSchemaNames SchemaNames { get; set; } = new DatabaseSchemaNames();
        
        [Required, IsValidDatabaseType()]
        public string DatabaseType { get; set; }
        
        public class DatabaseSchemaNames
        {
            [Required]
            public string ZebraPillarEmerald { get; set; } = "zpe";
        }
    }
}