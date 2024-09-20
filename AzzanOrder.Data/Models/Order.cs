using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzzanOrder.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        public int OrderDetail { get; set; }
        [ForeignKey("TableId")]
        public Table Table { get; set; }
        public int TableId { get; set; }
        [Required]
        public float Cost { get; set; }
        [Required]
        public float Tax { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; }
    }
}
