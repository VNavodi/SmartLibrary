using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartLibrary.API.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string ISBN { get; set; } = string.Empty;
        [Required]
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int PublishedYear { get; set; }
        public decimal Price { get; set; }
        public int AvailableCopies { get; set; }
        public bool IsAvailable { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [JsonIgnore]
        public virtual Author? Author { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [JsonIgnore]
        public virtual Category? Category { get; set; }
    }
}
