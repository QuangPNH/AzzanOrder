using System.ComponentModel.DataAnnotations;

namespace AzzanOrder.Data.Models
{
    public class MenuItem
    {
        [Key]
        public int MenuId { get; set; }
        [Required]
        public string MenuName { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; } 
        public float Discount { get; set; }
    }
}
