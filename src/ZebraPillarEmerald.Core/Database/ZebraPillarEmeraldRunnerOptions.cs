using FluentMigrator.Runner.Initialization;
using Microsoft.AspNetCore.Hosting;

namespace ZebraPillarEmerald.Core.Database
{
    public class ZebraPillarEmeraldRunnerOptions : RunnerOptions
    {
        public ZebraPillarEmeraldRunnerOptions(
            IWebHostEnvironment webHostEnvironment
            )
        {
            Profile = webHostEnvironment.EnvironmentName;
        }

        public ZebraPillarEmeraldRunnerOptions()
        {
            throw new System.NotImplementedException();
        }
    }
}