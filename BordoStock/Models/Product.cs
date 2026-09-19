using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BordoStock.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Code { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Barcode { get; set; }

    public int CategoryId { get; set; }
    public Category? Category { get; set; }

    public int? SupplierId { get; set; }
    public Supplier? Supplier { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PurchasePrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal SalePrice { get; set; }

    public int StockQuantity { get; set; }

    public int MinStockLevel { get; set; }

    [StringLength(30)]
    public string Unit { get; set; } = "Adet";

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(250)]
    public string? ImageUrl { get; set; }

    public bool IsActive { get; set; } = true;
}
