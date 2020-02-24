using System;

namespace ZebraPillarEmerald.Core.Attributes
{
    [AttributeUsageAttribute(AttributeTargets.Class)]
    public class ConfigurationSectionNameAttribute : Attribute
    {
        public ConfigurationSectionNameAttribute(string sectionName)
        {
            SectionName = sectionName;
        }
        
        public string SectionName { get; }
    }
}