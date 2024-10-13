using System;
using System.Collections.Generic;

namespace AzzanOrder.Data.Models
{
    public partial class About
    {
        public int AboutId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int? OwnerId { get; set; }

        public virtual Owner? Owner { get; set; }
    }
}
