using ISD_Project.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ISD_Project.Server.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        
        //Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,IConfiguration configuration) : base(options)
        {
            this._configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Role>().HasData(
                 new Role { Id = 1, Name = "Customer"},
                 new Role { Id = 2, Name = "Admin"}
                );
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
