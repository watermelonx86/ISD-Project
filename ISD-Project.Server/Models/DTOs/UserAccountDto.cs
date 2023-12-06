using System.ComponentModel.DataAnnotations;
using ISD_Project.Server.Models;

namespace ISD_Project.Server;

public class UserAccountDto
{
    [Key]
    public int Id { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; } = String.Empty;
    public int IsActivated { get; set; } = (int)AccountStatus.Inactive;
    public ICollection<UserRole>? UserRoles { get; set; }
    public int? UserId { get; set; }
}
