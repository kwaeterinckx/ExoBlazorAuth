using System.ComponentModel.DataAnnotations;

namespace ExoBlazor_FE.Models
{
    public class RegisterForm
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
