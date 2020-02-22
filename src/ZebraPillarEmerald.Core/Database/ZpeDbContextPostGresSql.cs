using Microsoft.EntityFrameworkCore;

namespace ZebraPillarEmerald.Core.Database
{
    public class ZpeDbContextPostGresSql : ZpeDbContext
    {
        public ZpeDbContextPostGresSql(DbContextOptions<ZpeDbContextPostGresSql> options) : base(options)
        {
        }
    }
}