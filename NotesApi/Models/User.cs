using System.Text.Json.Serialization;

namespace NotesApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}