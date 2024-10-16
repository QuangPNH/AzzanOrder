using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzzanOrder.Data.Models
{
    public partial class Member
    {
        public Member()
        {
            Feedbacks = new HashSet<Feedback>();
            Notifications = new HashSet<Notification>();
            Orders = new HashSet<Order>();
        }

        public int MemberId { get; set; }
        public string? MemberName { get; set; }
        public bool? Gender { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^(0)(\d{9})$", ErrorMessage = "Phone number must be 10 digits")]
        public string Phone { get; set; } = null!;
        public string? Gmail { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Address { get; set; }
        public int? Point { get; set; }
        public string? Image { get; set; }

        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
