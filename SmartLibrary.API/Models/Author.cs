using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartLibrary.API.Models
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Biography { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;
        [JsonIgnore]
        public virtual List<Book> Books { get; set; } = new List<Book>();
    }
}
