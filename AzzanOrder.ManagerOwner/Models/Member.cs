using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzzanOrder.ManagerOwner.Models
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
        [Required(ErrorMessage = "Phone number is requied.")]
        [StringLength(10, ErrorMessage = "Phone number must be 10 characters long.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string Phone { get; set; } = null!;
        public string? Gmail { get; set; }
        public DateTime? BirthDate { get; set; }
		public string? Address { get; set; }
		public double? Point { get; set; }
		public string? Image { get; set; }
		public bool? IsDelete { get; set; }

		public virtual ICollection<Feedback> Feedbacks { get; set; }
		public virtual ICollection<MemberVoucher> MemberVouchers { get; set; }
		public virtual ICollection<Notification> Notifications { get; set; }
		public virtual ICollection<Order> Orders { get; set; }
	}
}
