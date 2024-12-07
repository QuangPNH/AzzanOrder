using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzzanOrder.ManagerOwner.Models
{
    public partial class Table
    {
        public Table()
        {
            Orders = new HashSet<Order>();
        }

        public int TableId { get; set; }
        [Required(ErrorMessage = "Qr is required")]
        public string? Qr { get; set; } = null!;
        public bool? Status { get; set; }
        public int? EmployeeId { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
