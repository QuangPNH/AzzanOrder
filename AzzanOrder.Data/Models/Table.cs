using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AzzanOrder.Data.Models
{
    public partial class Table
    {
        public Table()
        {
            Orders = new HashSet<Order>();
        }

        public int TableId { get; set; }
       
        public string? Qr { get; set; } = null!;
        public bool? Status { get; set; }
        public int? EmployeeId { get; set; }

		[JsonIgnore]
		public virtual ICollection<Order> Orders { get; set; }
    }
}
