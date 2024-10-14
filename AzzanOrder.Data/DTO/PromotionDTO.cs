using AzzanOrder.Data.Models;

namespace AzzanOrder.Data.DTO
{
    public class PromotionDTO
    {
		public int PromotionId { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? Image { get; set; }
		public int? EmployeeId { get; set; }
	}
}
