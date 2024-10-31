using System;
using System.Collections.Generic;

namespace AzzanOrder.ManagerOwner.Models
{
    public partial class MemberVoucher
    {
        public int MemberId { get; set; }
        public int VoucherDetailId { get; set; }
        public bool? IsActive { get; set; }
        public int? OrderId { get; set; }

        public virtual Member Member { get; set; } = null!;
        public virtual Order? Order { get; set; }
        public virtual VoucherDetail VoucherDetail { get; set; } = null!;
    }
}
