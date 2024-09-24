using System;
using System.Collections.Generic;

namespace AzzanOrder.Data.Models
{
    public partial class ItemCategory
    {
        public ItemCategory()
        {
            MenuCategories = new HashSet<MenuCategory>();
            VocherDetails = new HashSet<VoucherDetail>();
        }

        public int ItemCategoryId { get; set; }
        public string? ItemCategoryName { get; set; }
        public string? Description { get; set; }
        public double? Discount { get; set; }
        public string? Image { get; set; }

        public virtual ICollection<MenuCategory> MenuCategories { get; set; }

        public virtual ICollection<VoucherDetail> VocherDetails { get; set; }
    }
}
