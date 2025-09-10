using System.ComponentModel.DataAnnotations;

namespace TrainingCenterAPI.DTOs.Auth
{
    public class RegisterDto
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Role { get; set; } // Admin / Teacher / Student

    }
}
