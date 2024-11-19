using System;
using System.Collections.Generic;

namespace AzzanOrder.Data.Models
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public bool? IsRead { get; set; }
		public int? MemberId { get; set; }
        public int? EmployeeId { get; set; }

        public virtual Employee? Employee { get; set; }
        public virtual Member? Member { get; set; }
    }
}
