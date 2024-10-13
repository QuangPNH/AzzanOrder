using System;
using System.Collections.Generic;

namespace AzzanOrder.Data.Models
{
    public partial class Voucher
    {
        public int VocherDetailId { get; set; }
        public int ItemCategoryId { get; set; }
        public bool? IsActive { get; set; }

        public virtual ItemCategory ItemCategory { get; set; } = null!;
        public virtual VoucherDetail VocherDetail { get; set; } = null!;
    }
}
