using System;
using FluentMigrator.Runner.Initialization;
using Microsoft.Extensions.Options;
using ZebraPillarEmerald.Core.Settings;

namespace ZebraPillarEmerald.Core.Database
{
    /// <summary>This provides the FluentMigrator runner with a way to get the connection string.</summary>
    public class ZebraPillarEmeraldConnectionStringReader : IConnectionStringReader
    {
        private readonly ConnectionStringsSettings _connectionStringSettings;
        private readonly DatabaseSettings _databaseSettings;

        public ZebraPillarEmeraldConnectionStringReader(
            IOptionsSnapshot<DatabaseSettings> databaseSettingsAccessor,
            IOptionsSnapshot<ConnectionStringsSettings> connectionSettingsAccessor
            )
        {
            _databaseSettings = databaseSettingsAccessor.Value;
            _connectionStringSettings = connectionSettingsAccessor.Value;
        }
        
        public string GetConnectionString(string connectionStringOrName)
        {
            switch (_databaseSettings.DatabaseType)
            {
                case DatabaseTypes.PostgreSQL:
                case DatabaseTypes.SQLite:
                case DatabaseTypes.SQLServer:
                    return _connectionStringSettings.ZebraPillarEmerald;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(_databaseSettings.DatabaseType));
            }
        }

        public int Priority { get; } = 1;
    }
}