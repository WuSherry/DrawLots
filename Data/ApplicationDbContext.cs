using DrawLots.Models;
using Microsoft.EntityFrameworkCore;

namespace DrawLots.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Lots> Lots { get; set; }
        public DbSet<Activities> Activities { get; set; }

        public DbSet<LotRecord> LotRecord { get; set; }


    }
}
