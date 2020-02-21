using Microsoft.EntityFrameworkCore;
using ZebraPillarEmerald.Core.Models;

namespace ZebraPillarEmerald.Core.Database
{
    public class ZpeDbContext : DbContext
    {
        private readonly string _nameOrConnectionString;

        public ZpeDbContext(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
    }
}