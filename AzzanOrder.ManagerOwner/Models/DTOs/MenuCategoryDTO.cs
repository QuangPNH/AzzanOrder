namespace AzzanOrder.Data.DTO
{
    public class MenuCategoryDTO
    {
        public int MenuItemId { get; set; }
        public int ItemCategoryId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsForCombo { get; set; }
    }
}
