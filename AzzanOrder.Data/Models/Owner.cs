using System;
using System.Collections.Generic;

namespace AzzanOrder.Data.Models
{
    public partial class Owner
    {
        public Owner()
        {
            Abouts = new HashSet<About>();
        }

        public int OwnerId { get; set; }
        public string? OwnerName { get; set; }
        public bool? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Gmail { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? BankId { get; set; }
        public string? Image { get; set; }

        public virtual Bank? Bank { get; set; }
        public virtual ICollection<About> Abouts { get; set; }
    }
}
