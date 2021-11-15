using Microsoft.EntityFrameworkCore;
namespace App.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }
        
        public DbSet<RegistrationUrl> RegistrationUrls { get; set; }
        public DbSet<ListUrl> Lists { get; set; }
        public DbSet<Denylist> Denylists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {  
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RegistrationUrl>().ToTable("RegistrationUrl");
            modelBuilder.Entity<ListUrl>().ToTable("ListUrl");
            modelBuilder.Entity<Denylist>().ToTable("DenyList");
        }
    }
}