using System.ComponentModel.DataAnnotations;

namespace AbySalto.Junior.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }
        
        public Order Order { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity {  get; set; }

        public int ArticleId { get; set; }

        public Article Article { get; set; }
    }
}
