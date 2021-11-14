using Microsoft.EntityFrameworkCore;
namespace App.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        
        public DbSet<registration_url> RegistrationUrls { get; set; }
        public DbSet<list> Lists { get; set; }
        public DbSet<denylist> Denylists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<registration_url>().has
            modelBuilder.Entity<registration_url>().ToTable("registration_url").HasKey(x => x.id);
            modelBuilder.Entity<denylist>().ToTable("denylist").HasKey(x => x.id);
            modelBuilder.Entity<list>().ToTable("list").HasKey(x => x.id);
        }
    }
}