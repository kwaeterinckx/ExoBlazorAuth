using System.ComponentModel.DataAnnotations;

namespace ExoBlazor_API.Models.DTOs
{
    public class UserLoginDTO
    {
        [Required]
        public string Login {  get; set; }
        public string Password { get; set; }
    }
}
