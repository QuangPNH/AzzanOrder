using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzzanOrder.Data.Models
{
    public class VoucherDetail
    {
        [Key]
        public int VoucherMenuId { get; set; }
        [ForeignKey("VoucherId")]
        public Voucher Voucher { get; set; }
        public int VoucherId { get; set; }
        [ForeignKey("ItemCategoryId")]
        public ItemCategory ItemCategory { get; set; }
        public int ItemCategoryId { get; set; }
    }
}
