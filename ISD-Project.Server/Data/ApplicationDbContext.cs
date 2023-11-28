using ISD_Project.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ISD_Project.Server.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        //Constructor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
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
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserAccount)
                .WithOne(ua => ua.User)
                .HasForeignKey<UserAccount>(ua => ua.UserId)
                .IsRequired(false);

            modelBuilder.Entity<Customer>()
                .HasBaseType<User>();

            modelBuilder.Entity<Admin>()
                .HasBaseType<User>();

            modelBuilder.Entity<FinancialDepartment>()
                .HasBaseType<User>();

            modelBuilder.Entity<ValidationDepartment>()
                .HasBaseType<User>();

            modelBuilder.Entity<CustomerCareDepartment>()
                .HasBaseType<User>();

            modelBuilder.Entity<Role>().HasData(
                 new Role { Id = 1, Name = "Customer" },
                 new Role { Id = 2, Name = "Admin" },
                 new Role { Id = 3, Name = "FinancialDepartment" },
                 new Role { Id = 4, Name = "ValidationDepartment" },
                 new Role { Id = 5, Name = "CustomerCareDepartment" }
                );
           
        }
        //User
        public DbSet<User> Users { get; set; }
        // Child classes of User
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<FinancialDepartment> FinancialDepartments { get; set; }
        public DbSet<ValidationDepartment> ValidationDepartments { get; set; }
        public DbSet<CustomerCareDepartment> CustomerCareDepartments { get; set; }
        //
        public DbSet<HealthInformation> HealthInformation { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
