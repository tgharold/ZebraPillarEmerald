using Microsoft.Extensions.Options;

namespace ZebraPillarEmerald.Core.Options.Validation
{
    public class DatabaseOptionsValidator : IValidateOptions<DatabaseOptions>
    {
        public ValidateOptionsResult Validate(string name, DatabaseOptions options)
        {
            throw new System.NotImplementedException();
        }
    }
}