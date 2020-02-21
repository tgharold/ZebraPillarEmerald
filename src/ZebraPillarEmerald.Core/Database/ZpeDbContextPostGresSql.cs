namespace ZebraPillarEmerald.Core.Database
{
    public class ZpeDbContextPostGresSql : ZpeDbContext
    {
        public ZpeDbContextPostGresSql(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }
    }
}