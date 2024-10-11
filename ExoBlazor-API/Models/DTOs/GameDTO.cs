using System.ComponentModel.DataAnnotations;

namespace ExoBlazor_API.Models.DTOs
{
    public class GameDTO
    {
        [Required]
        public string Title { get; set; }

        public string? Synopsis { get; set; }

        [Required]
        public int ReleaseYear { get; set; }
    }
}
