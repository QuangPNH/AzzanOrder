using AzzanOrder.ManagerOwner.Models;

namespace AzzanOrder.ManagerOwner.DTOs
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
