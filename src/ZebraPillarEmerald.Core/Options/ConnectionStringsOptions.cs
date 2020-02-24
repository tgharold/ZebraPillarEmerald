using System;
using ZebraPillarEmerald.Core.Attributes;
using ZebraPillarEmerald.Core.Interfaces;

namespace ZebraPillarEmerald.Core.Options
{
    [ConfigurationSectionName("ConnectionStrings")]
    public class ConnectionStringsOptions : ICanValidate
    {
        public string ZebraPillarEmerald { get; set; }
        
        public bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}