using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BordoStock.Models;

public class StockMovement
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public int WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }

    [Required]
    [StringLength(20)]
    public string MovementType { get; set; } = "In";

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    public DateTime MovementDate { get; set; } = DateTime.Now;

    [StringLength(250)]
    public string? Description { get; set; }
}
