using System;
using System.Collections.Generic;

namespace AzzanOrder.Data.Models
{
    public partial class VoucherDetail
    {
        public VoucherDetail()
        {
            MemberVouchers = new HashSet<MemberVoucher>();
            Vouchers = new HashSet<Voucher>();
        }

        public int VoucherDetailId { get; set; }
        public string? Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? Discount { get; set; }
        public double? Price { get; set; }
        public int? EmployeeId { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual ICollection<MemberVoucher> MemberVouchers { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
