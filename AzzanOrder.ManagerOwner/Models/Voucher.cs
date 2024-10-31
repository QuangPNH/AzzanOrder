using System;
using System.Collections.Generic;

namespace AzzanOrder.ManagerOwner.Models
{
    public partial class Voucher
    {
        public int VoucherDetailId { get; set; }
        public int ItemCategoryId { get; set; }
        public bool? IsActive { get; set; }

        public virtual ItemCategory ItemCategory { get; set; } = null!;
        public virtual VoucherDetail VoucherDetail { get; set; } = null!;
    }
}
