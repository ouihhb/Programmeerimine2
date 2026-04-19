using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Application.Data
{
    public class Product : Entity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Range(0, 100000)]
        public decimal Price { get; set; }
    }
}