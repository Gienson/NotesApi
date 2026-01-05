using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NotesApi.Models
{
    public class Note
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        [JsonIgnore]
        public int UserId { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
    }
}