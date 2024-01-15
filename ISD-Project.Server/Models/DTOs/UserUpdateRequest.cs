using System.ComponentModel.DataAnnotations;

namespace ISD_Project.Server.Models.DTOs
{
    public class UserUpdateModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;
        [EmailAddress]

        public string Email { get; set; } = String.Empty;
        [RegularExpression(@"^\+84[0-9]*$", ErrorMessage = "The phone number must start with the country code +84.")]
        [StringLength(13)]

        public string Phone { get; set; } = String.Empty;

        public int? Gender { get; set; } = (int)GenderType.Male;

        public string Address { get; set; } = String.Empty;
    }
}