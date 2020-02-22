namespace ZebraPillarEmerald.Core.Database
{
    public class DatabaseSettings
    {
        public SchemaNames SchemaNames { get; set; } = new SchemaNames(); 
        public string DatabaseType { get; set; }
    }

    public class SchemaNames
    {
        public string ZebraPillarEmerald { get; set; } = "zpe";
    }
}