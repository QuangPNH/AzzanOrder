using System.ComponentModel.DataAnnotations;

namespace AzzanOrder.Data.Models
{
    public class ItemCategory
    {
        [Key]
        public int ItemCategoryId { get; set; }
        [Required]
        public string ItemCategoryName { get; set; }
        [Required]
        public float Discount { get; set; }
    }
}
