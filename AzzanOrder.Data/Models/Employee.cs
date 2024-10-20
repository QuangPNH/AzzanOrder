﻿using System;
using System.Collections.Generic;

namespace AzzanOrder.Data.Models
{
    public partial class Employee
    {
        public Employee()
        {
            InverseManager = new HashSet<Employee>();
            MenuItems = new HashSet<MenuItem>();
            Notifications = new HashSet<Notification>();
            Promotions = new HashSet<Promotion>();
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
        public string? Image { get; set; }
        public int? ManagerId { get; set; }
        public int? OwerId { get; set; }
        public bool? IsDelete { get; set; }

        public virtual Employee? Manager { get; set; }
        public virtual Owner? Ower { get; set; }
        public virtual Role? Role { get; set; }
        public virtual ICollection<Employee> InverseManager { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Promotion> Promotions { get; set; }
    }
}
