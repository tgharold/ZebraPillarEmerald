using System.ComponentModel.DataAnnotations;
using ZebraPillarEmerald.Core.Attributes;

namespace ZebraPillarEmerald.Core.Settings
{
    [ConfigurationSectionName("ConnectionStrings")]
    public class ConnectionStringsSettings
    {
        [Required]
        public string ZebraPillarEmerald { get; set; }
    }
}