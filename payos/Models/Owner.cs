using System;
using System.Collections.Generic;

namespace payos.Models
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
		public string Phone { get; set; } = null!;
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
