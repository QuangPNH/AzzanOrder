using System;
using System.Collections.Generic;

namespace AzzanOrder.ManagerOwner.Models
{
    public partial class Promotion
    {
        public int PromotionId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public int? EmployeeId { get; set; }
        public bool? IsActive { get; set; }
        public virtual Employee? Employee { get; set; }
    }
}
