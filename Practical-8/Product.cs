using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Models;

public class Product
{
    public int ProductId { get; set; }

    [Required, StringLength(100)]
    [Display(Name = "Product Name")]
    public string ProductName { get; set; } = string.Empty;

    [Required, StringLength(50)]
    public string Category { get; set; } = string.Empty;

    [Range(0.01, 999999999)]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue)]
    public int Quantity { get; set; }

    [Required, StringLength(100)]
    public string Supplier { get; set; } = string.Empty;
}
