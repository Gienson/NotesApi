using System.ComponentModel.DataAnnotations;

namespace NotesApi.Models
{
    public class NoteDto
    {
        [Required]
        public string Content { get; set; } = string.Empty;
    }
}