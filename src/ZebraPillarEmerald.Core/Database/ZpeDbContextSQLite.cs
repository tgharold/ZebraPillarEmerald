using Microsoft.EntityFrameworkCore;

namespace ZebraPillarEmerald.Core.Database
{
    public class ZpeDbContextSQLite : ZpeDbContext
    {
        public ZpeDbContextSQLite(
            DbContextOptions<ZpeDbContextSQLite> options
            ) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureForSqlite();
        }

        internal static void ConfigureForSqlite()
        {
            
        }
    }
}