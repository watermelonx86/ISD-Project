using System.ComponentModel.DataAnnotations;

namespace ISD_Project.Server.Models.DTOs
{
    public class CustomerRegisterRequest
    {
        [Required]
        public string Name { get; set; } = String.Empty;
        [Required]
        public int Gender { get; set; } = (int)GenderType.Male;

        [Required, EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Required]
        [StringLength(12)]
        public string IdentityDocumentId { get; set; } = String.Empty;

        public string Address { get; set; } = String.Empty;
        [Required]
        [RegularExpression(@"^\+84[0-9]*$", ErrorMessage = "The phone number must start with the country code +84.")]
        [StringLength(13)]
        public string PhoneNumber { get; set; } = String.Empty;
    }
}
