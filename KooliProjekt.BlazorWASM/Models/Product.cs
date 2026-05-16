using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.BlazorWASM.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(255)]
        public string Description { get; set; } = string.Empty;

        [Range(0, 100000)]
        public decimal Price { get; set; }
    }
}
