using System.ComponentModel.DataAnnotations;

namespace p6.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(1, 1000000)]
        public decimal Price { get; set; }

        [Required]
        public string category { get; set; }

        public string Description { get; set; }
    }
}
