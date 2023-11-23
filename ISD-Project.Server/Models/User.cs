using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISD_Project.Server.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(12)]
        public string IdentityDocumentId { get; set; } = String.Empty;
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public int Gender { get; set; } = (int)GenderType.Male;
        public string Address { get; set; } = String.Empty;
        [Required]
        [RegularExpression(@"^\+84[0-9]*$", ErrorMessage = "The phone number must start with the country code +84.")]
        [StringLength(13)]
        public string PhoneNumber { get; set; } = String.Empty;
        public UserAccount? UserAccount { get; set; }
    }
    public enum GenderType
    {
        Male = 0,
        Female = 1,
    }
}

