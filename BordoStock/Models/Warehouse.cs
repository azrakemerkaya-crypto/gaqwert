using System.ComponentModel.DataAnnotations;

namespace BordoStock.Models;

public class Warehouse
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [StringLength(250)]
    public string? Address { get; set; }

    public bool IsActive { get; set; } = true;
}
