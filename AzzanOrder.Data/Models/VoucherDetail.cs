using System;
using System.Collections.Generic;

namespace AzzanOrder.Data.Models
{
    public partial class VoucherDetail
    {
        public VoucherDetail()
        {
            Vouchers = new HashSet<Voucher>();
        }

        public int VoucherDetailId { get; set; }
        public string? Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? Discount { get; set; }

        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
