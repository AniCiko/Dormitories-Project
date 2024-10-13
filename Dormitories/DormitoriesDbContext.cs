using Dormitories.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dormitories
{
    public class DormitoriesDbContext : DbContext
    {
        public DormitoriesDbContext()
        {
        }
        public DormitoriesDbContext(DbContextOptions<DormitoriesDbContext> options) : base(options)
        { }
        public DbSet<Announcements> Announcements { get; set; }
        public DbSet<Applications> Applications { get; set; }
        public DbSet<Entities.Dormitories> Dormitories { get; set; }
        public DbSet<Students> Students { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
