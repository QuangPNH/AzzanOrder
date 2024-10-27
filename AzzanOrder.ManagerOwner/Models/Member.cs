using System;
using System.Collections.Generic;

namespace AzzanOrder.Data.Models
{
    public partial class Member
    {
        public Member()
        {
            Feedbacks = new HashSet<Feedback>();
            MemberVouchers = new HashSet<MemberVoucher>();
            Notifications = new HashSet<Notification>();
            Orders = new HashSet<Order>();
        }

        public int MemberId { get; set; }
        public string? MemberName { get; set; }
        public bool? Gender { get; set; }
        public string Phone { get; set; } = null!;
        public string? Gmail { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Address { get; set; }
        public int? Point { get; set; }
        public string? Image { get; set; }
        public bool? IsDelete { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<MemberVoucher> MemberVouchers { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
