using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace SmartLibrary.API.Models
{
    public class BorrowRecord
    {
        public int Id { get; set; }
        [Required]
        public int MemberId { get; set; }
        [JsonIgnore]
        public virtual Member? Member { get; set; }
        [Required]
        public int BookId { get; set; }
        [JsonIgnore]
        public virtual Book? Book { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsReturned { get; set; }
    }
}
