using System;
using System.Collections.Generic;

namespace AzzanOrder.Data.Models
{
    public partial class ItemCategory
    {
        public ItemCategory()
        {
            Vouchers = new HashSet<Voucher>();
            MenuCategories = new HashSet<MenuCategory>();
        }

        public int ItemCategoryId { get; set; }
        public string? ItemCategoryName { get; set; }
        public string? Description { get; set; }
        public double? Discount { get; set; }
        public string? Image { get; set; }
        public int? EmployeeId { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsCombo { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
        public virtual ICollection<MenuCategory> MenuCategories { get; set; }
    }
}
