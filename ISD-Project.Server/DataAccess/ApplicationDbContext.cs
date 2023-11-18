using Microsoft.EntityFrameworkCore;

namespace ISD_Project.Server.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        
        //Constructor
        public ApplicationDbContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        }

    }
}
