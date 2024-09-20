using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzzanOrder.Data.Models
{
    public class VNPay
    {
        [Key]
        public int BankId { get; set; }
        [Required]
        public string BankName { get; set; }
        [Required]
        public string BankNumber { get; set; }
        [Required]
        public string AccountName { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public int UserId { get; set; } 
    }
}
