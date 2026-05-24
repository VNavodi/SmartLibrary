using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SmartLibrary.API.Models
{
    public class Member
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime MembershipDate { get; set; }
        [JsonIgnore]
        public virtual List<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
    }
}
