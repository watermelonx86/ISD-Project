using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace ISD_Project.Server.Models
{
    public class UserRegisterRequest
    {
        //Validates that the email is in the correct format
        [Required, EmailAddress]
        public string Email { get; set; } = String.Empty;
        [Required]
        public string Password { get; set; } = String.Empty;
        //Makes sure that the password and confirm password are the same
        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = String.Empty;
    }

    
}
