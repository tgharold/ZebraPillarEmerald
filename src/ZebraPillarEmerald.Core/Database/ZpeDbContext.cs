using System;
using Microsoft.EntityFrameworkCore;
using ZebraPillarEmerald.Core.Models;

namespace ZebraPillarEmerald.Core.Database
{
    public class ZpeDbContext : DbContext
    {
        public ZpeDbContext(DbContextOptions<ZpeDbContext> options) : base(options)
        {
        }
        
        protected ZpeDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            throw new Exception("This method must be overriden as we have database typenames which need to be defined");
        }
    }
}