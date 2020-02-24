using ZebraPillarEmerald.Core.Attributes;

namespace ZebraPillarEmerald.Core.Options
{
    [ConfigurationSectionName("ConnectionStrings")]
    public class ConnectionStringsOptions
    {
        public string ZebraPillarEmerald { get; set; }
    }
}