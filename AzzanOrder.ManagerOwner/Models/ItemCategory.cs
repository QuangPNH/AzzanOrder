using System;
using System.Collections.Generic;

namespace AzzanOrder.ManagerOwner.Models
{
    public partial class ItemCategory
    {
        public ItemCategory()
        {
            MenuCategories = new HashSet<MenuCategory>();
            Vouchers = new HashSet<Voucher>();
        }

        public int ItemCategoryId { get; set; }
        public string? ItemCategoryName { get; set; }
        public string? Description { get; set; }
        public double? Discount { get; set; }
        public string? Image { get; set; }

        public virtual ICollection<MenuCategory> MenuCategories { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
