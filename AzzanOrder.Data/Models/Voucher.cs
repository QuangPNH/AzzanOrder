using System.ComponentModel.DataAnnotations;

namespace AzzanOrder.Data.Models
{
    public class Voucher
    {
        [Key]
        public int VoucherId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public float Discount { get; set; }
    }
}
