using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbySalto.Junior.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string BuyerName { get; set; }
        
        [Required]
        public DateTime OrderTime { get; set; }

        [Required]
        [StringLength(150)]
        public string Addres {  get; set; }

        [Required]
        public string ContactNumber {  get; set; }
        
        public string? Remark {  get; set; }

        [Required]
        public decimal Amount {  get; set; }

        [Required]
        public int PaymentTypeId { get; set; }
        public PaymentType PaymentType { get; set; }

        [Required]
        public int StatusId { get; set; }
        public Status Status { get; set; }
        [Required]
        public int CurrencyId { get; set; }

        public Currency Currency { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
