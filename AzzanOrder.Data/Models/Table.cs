using System.ComponentModel.DataAnnotations;

namespace AzzanOrder.Data.Models
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }
        [Required]
        public string QR { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
