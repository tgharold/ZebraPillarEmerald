using System.ComponentModel.DataAnnotations;
using OptionsPatternValidation;

namespace ZebraPillarEmerald.Core.Settings
{
    [SettingsSectionName("ConnectionStrings")]
    public class ConnectionStringsSettings
    {
        [Required]
        public string ZebraPillarEmerald { get; set; }
    }
}