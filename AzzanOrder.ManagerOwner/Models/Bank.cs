using System;
using System.Collections.Generic;

namespace AzzanOrder.ManagerOwner.Models
{
    public partial class Bank
    {
        public Bank()
        {
            Owners = new HashSet<Owner>();
        }

        public int BankId { get; set; }
        public string? BankName { get; set; }
        public string? BankNumber { get; set; }

        public virtual ICollection<Owner> Owners { get; set; }
    }
}
