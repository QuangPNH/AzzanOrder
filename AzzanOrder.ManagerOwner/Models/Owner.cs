using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzzanOrder.ManagerOwner.Models
{
    public partial class Owner
    {
		public Owner()
		{
			Abouts = new HashSet<About>();
			Employees = new HashSet<Employee>();
		}

		public int OwnerId { get; set; }
		public string? OwnerName { get; set; }
		public bool? Gender { get; set; }
        [Required(ErrorMessage = "Phone number is requied.")]
        [StringLength(10, ErrorMessage = "Phone number must be 10 characters long.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "Email address is required.")]
        [StringLength(50, ErrorMessage = "Email cannot exceed 50 characters.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Gmail { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
		public int? BankId { get; set; }
		public string? Image { get; set; }
		public bool? IsDelete { get; set; }
		public DateTime SubscriptionStartDate { get; set; }
		public DateTime SubscribeEndDate { get; set; }
		public bool? IsFreeTrial { get; set; }
		public int? NumberOfAccountAllowed { get; set; }

		public virtual Bank? Bank { get; set; }
		public virtual ICollection<About> Abouts { get; set; }
		public virtual ICollection<Employee> Employees { get; set; }
	}
}
