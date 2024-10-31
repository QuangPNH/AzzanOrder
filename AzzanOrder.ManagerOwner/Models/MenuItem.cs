using System;
using System.Collections.Generic;

namespace AzzanOrder.ManagerOwner.Models
{
    public partial class MenuItem
    {
        public MenuItem()
        {
            MenuCategories = new HashSet<MenuCategory>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int MenuItemId { get; set; }
        public string? ItemName { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }
        public double? Discount { get; set; }
        public string? Image { get; set; }
        public bool? IsAvailable { get; set; }
        public int? EmployeeId { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual ICollection<MenuCategory> MenuCategories { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
