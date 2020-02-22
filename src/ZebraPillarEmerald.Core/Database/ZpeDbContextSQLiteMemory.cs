using Microsoft.EntityFrameworkCore;

namespace ZebraPillarEmerald.Core.Database
{
    public class ZpeDbContextSQLiteMemory : ZpeDbContext
    {
        public ZpeDbContextSQLiteMemory(DbContextOptions<ZpeDbContextSQLiteMemory> options) : base(options)
        {
        }
    }
}