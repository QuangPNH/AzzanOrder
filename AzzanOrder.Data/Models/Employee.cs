using System;
using System.Collections.Generic;

namespace AzzanOrder.Data.Models
{
    public partial class Employee
    {
        public Employee()
        {
            MenuItems = new HashSet<MenuItem>();
            Notifications = new HashSet<Notification>();
        }

        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public bool? Gender { get; set; }
        public string? Phone { get; set; }
        public string? Gmail { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? RoleId { get; set; }
        public string? HomeAddress { get; set; }
        public string? WorkAddress { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
