namespace AzzanOrder.Data.DTO
{
    public class MenuItemDTO
	{
        public string? Category { get; set; }
        public int MenuItemId { get; set; }
		public string? ItemName { get; set; }
		public double? Price { get; set; }
		public string? Description { get; set; }
		public double? Discount { get; set; }
		public bool? IsAvailable { get; set; }
		public int? EmployeeId { get; set; }
		public string? ImageBase64 { get; set; }
	}
    public class MenuItemAddDTO
    {
        public string? ItemName { get; set; }
        public double? Price { get; set; }
        public string? Description { get; set; }
        public double? Discount { get; set; }
        public bool? IsAvailable { get; set; }
        public string? ImageBase64 { get; set; }
    }
}
