using System;
using System.Collections.Generic;

namespace AzzanOrder.Data.Models
{
    public partial class Role
    {
        public Role()
        {
            Employees = new HashSet<Employee>();
        }

        public int RoleId { get; set; }
        public string? RoleName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
