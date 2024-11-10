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
        public string? PAYOS_CLIENT_ID { get; set; }
        public string? PAYOS_API_KEY { get; set; }
        public string? PAYOS_CHECKSUM_KEY { get; set; }

        public virtual ICollection<Owner> Owners { get; set; }
    }
}
