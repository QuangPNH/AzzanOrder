using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzzanOrder.ManagerOwner.Models
{
	public partial class Employee
	{
		public Employee()
		{
			InverseManager = new HashSet<Employee>();
			ItemCategories = new HashSet<ItemCategory>();
			MenuItems = new HashSet<MenuItem>();
			Notifications = new HashSet<Notification>();
			Promotions = new HashSet<Promotion>();
			VoucherDetails = new HashSet<VoucherDetail>();
		}

		public int EmployeeId { get; set; }
		
		public string? EmployeeName { get; set; }
		public bool? Gender { get; set; }
		[Required(ErrorMessage = "Phone number is requied.")]
		[StringLength(10, ErrorMessage = "Phone number must be 10 characters long.")]
		[RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone number must be exactly 10 digits.")]
        
        public string Phone { get; set; } = null!;
		[Required(ErrorMessage = "Email address is required.")]
		[StringLength(50, ErrorMessage = "Email cannot exceed 50 characters.")]
		[EmailAddress(ErrorMessage = "Invalid email format.")]
		public string Gmail { get; set; } = null!;
		public DateTime? BirthDate { get; set; }
		[Required(ErrorMessage = "Role is required.")]
		public int RoleId { get; set; }
		public string? HomeAddress { get; set; }
		public string? WorkAddress { get; set; }
		public string? Image { get; set; }
		public int? ManagerId { get; set; }
		public int? OwnerId { get; set; }
		public bool? IsDelete { get; set; }

		public virtual Employee? Manager { get; set; }
		public virtual Owner? Owner { get; set; }
		public virtual Role Role { get; set; } = null!;
		public virtual ICollection<Employee> InverseManager { get; set; }
		public virtual ICollection<ItemCategory> ItemCategories { get; set; }
		public virtual ICollection<MenuItem> MenuItems { get; set; }
		public virtual ICollection<Notification> Notifications { get; set; }
		public virtual ICollection<Promotion> Promotions { get; set; }
		public virtual ICollection<VoucherDetail> VoucherDetails { get; set; }
	}
}
