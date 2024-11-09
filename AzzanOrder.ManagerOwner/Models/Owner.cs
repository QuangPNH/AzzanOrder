using System;
using System.Collections.Generic;

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
        public string? Phone { get; set; }
        public string? Gmail { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? BankId { get; set; }
        public string? Image { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime SubscriptionEndDate { get; set; }

        public virtual Bank? Bank { get; set; }
        public virtual ICollection<About> Abouts { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
