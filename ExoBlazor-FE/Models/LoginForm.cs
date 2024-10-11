using System.ComponentModel.DataAnnotations;

namespace ExoBlazor_FE.Models
{
    public class LoginForm
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
