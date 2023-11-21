namespace ISD_Project.Server.Models
{

    public class UserRole
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }

    public enum RoleType
    {
        Customer = 1,
        Admin = 2
    }
}
