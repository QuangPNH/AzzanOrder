using System;
using System.Collections.Generic;

namespace AzzanOrder.Data.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int? Quantity { get; set; }
        public int? MenuItemId { get; set; }
        public int? OrderId { get; set; }
        public bool? Status { get; set; }

        public virtual MenuItem? MenuItem { get; set; }
        public virtual Order? Order { get; set; }
    }
}
