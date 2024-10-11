using System.ComponentModel.DataAnnotations;

namespace ExoBlazor_API.Models.DTOs
{
    public class UserRegisterDTO
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
