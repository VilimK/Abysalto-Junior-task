using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbySalto.Junior.Models
{
    public class Article 
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }
        
        public string? Description {  get; set; }
    }
}
