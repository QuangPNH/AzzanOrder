using System.ComponentModel.DataAnnotations;

namespace AzzanOrder.Data.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public int Role { get; set; }
        public int Point { get; set; }
    }
}
