using System.ComponentModel.DataAnnotations;

namespace AbySalto.Junior.Models
{
    public class PaymentType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(150)]
        public string Description { get; set; }
    }
}
