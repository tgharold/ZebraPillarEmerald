using System;
using ZebraPillarEmerald.Core.Attributes;

namespace ZebraPillarEmerald.Core.Options
{
    [ConfigurationSectionName("ConnectionStrings")]
    public class ConnectionStringsOptions : ICanValidate
    {
        public string ZebraPillarEmerald { get; set; }
        
        public bool Validate()
        {
            throw new NotImplementedException();
        }
    }
}