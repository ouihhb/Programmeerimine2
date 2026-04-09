using System.ComponentModel.DataAnnotations;
using KooliProjekt.Application.Infrastructure.Paging;
public class Product
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(255)]
    public string Description { get; set; }

    [Range(0, 100000)]
    public decimal Price { get; set; }
}