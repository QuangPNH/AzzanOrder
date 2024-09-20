using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AzzanOrder.Data.Models
{
    public class MenuCategory
    {
        [ForeignKey("MenuId")]
        public MenuItem MenuItem { get; set; }
        public int MenuId { get; set; }
        [ForeignKey("CategoryId")]
        public ItemCategory Category { get; set; }
        public int CategoryId { get; set; }
    }
}
