using AzzanOrder.ManagerOwner.DTOs;

namespace AzzanOrder.ManagerOwner.Models
{
    public class Dashbroad
	{
		public IEnumerable<MenuItemSalesDTO> trendingItems { get; set; }
		public IEnumerable<MenuItemSalesDTO> failingItems { get; set; }
		public int numberOfOrder { get; set; }
		public int numberOfEmployee { get; set; }
		public double PercentileChangeInNumberOfOrder { get; set; }
		public int numberOFManger { get; set; }
		public List<int> MonthlySales { get; set; }
	}
}
