using System.ComponentModel.DataAnnotations;

namespace NotesApi.Models
{
    public class UserDto
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}