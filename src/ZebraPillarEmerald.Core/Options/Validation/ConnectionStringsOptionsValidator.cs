using Microsoft.Extensions.Options;

namespace ZebraPillarEmerald.Core.Options.Validation
{
    public class ConnectionStringsOptionsValidator : IValidateOptions<ConnectionStringsOptions>
    {
        public ValidateOptionsResult Validate(string name, ConnectionStringsOptions options)
        {
            throw new System.NotImplementedException();
        }
    }
}