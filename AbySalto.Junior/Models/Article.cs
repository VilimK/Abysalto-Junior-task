using System.ComponentModel.DataAnnotations;

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
        public decimal Price { get; set; }
        public string? Description {  get; set; }
        public List<Article> Articles { get; set; }
    }
}
