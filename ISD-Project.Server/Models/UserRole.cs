using System.ComponentModel.DataAnnotations;

namespace ISD_Project.Server.Models
{

    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserAccount User { get; set; } = null!;

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }

    public enum RoleType
    {
        Customer = 1,
        Admin = 2,
        FinancialDepartment = 3,
        ValidationDepartment = 4,
        CustomerCareDepartment = 5
    }
}
