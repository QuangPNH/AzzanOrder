using System;
using System.Collections.Generic;

namespace payos.Models
{
	public partial class MenuCategory
	{
		public int MenuItemId { get; set; }
		public int ItemCategoryId { get; set; }

		public virtual ItemCategory ItemCategory { get; set; } = null!;
		public virtual MenuItem MenuItem { get; set; } = null!;
	}
}
