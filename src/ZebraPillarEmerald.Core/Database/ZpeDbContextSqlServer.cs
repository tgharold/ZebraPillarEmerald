namespace ZebraPillarEmerald.Core.Database
{
    public class ZpeDbContextSqlServer : ZpeDbContext
    {
        public ZpeDbContextSqlServer(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }
    }
}