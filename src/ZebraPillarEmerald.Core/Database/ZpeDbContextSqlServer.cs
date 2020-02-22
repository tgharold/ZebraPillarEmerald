using Microsoft.EntityFrameworkCore;

namespace ZebraPillarEmerald.Core.Database
{
    public class ZpeDbContextSqlServer : ZpeDbContext
    {
        public ZpeDbContextSqlServer(DbContextOptions<ZpeDbContextSqlServer> options) : base(options)
        {
        }
    }
}