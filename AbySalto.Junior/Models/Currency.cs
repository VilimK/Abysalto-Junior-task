using System.ComponentModel.DataAnnotations;

namespace AbySalto.Junior.Models
{
    public class Currency
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(3)]
        public string Code {  get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }
    
    }
}
