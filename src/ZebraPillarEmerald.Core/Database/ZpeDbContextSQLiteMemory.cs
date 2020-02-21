namespace ZebraPillarEmerald.Core.Database
{
    public class ZpeDbContextSQLiteMemory : ZpeDbContext
    {
        public ZpeDbContextSQLiteMemory(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }
    }
}