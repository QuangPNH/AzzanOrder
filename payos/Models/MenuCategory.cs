using System;
using System.Collections.Generic;

namespace payos.Models
{
    public partial class MenuCategory
    {
        public int MenuItemId { get; set; }
        public int ItemCategoryId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsForCombo { get; set; }

        public virtual ItemCategory ItemCategory { get; set; } = null!;
        public virtual MenuItem MenuItem { get; set; } = null!;
    }
}
