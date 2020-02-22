using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace ZebraPillarEmerald.Core.Database
{
    public class ZpeDbContextSQLiteMemory : ZpeDbContext, IDisposable
    {
        /// <summary>
        /// Sqlite will close/delete memory/temporary databases when the last connection
        /// is closed. We use a hidden connection variable to keep the database
        /// intact until we no longer need it.
        /// </summary>                                               Memory
        private readonly SqliteConnection _holdOpenConnection;
        
        public ZpeDbContextSQLiteMemory(
            DbContextOptions<ZpeDbContextSQLiteMemory> options,
            ConnectionStringSettings connectionStringSettings
            ) : base(options)
        {
            _holdOpenConnection = new SqliteConnection(connectionStringSettings.ZebraPillarEmerald);
            _holdOpenConnection.Open();            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ZpeDbContextSQLite.ConfigureForSqlite();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _holdOpenConnection?.Dispose();
            }
        }

        public sealed override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}