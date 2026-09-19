using System.ComponentModel.DataAnnotations;

namespace BordoStock.Models;

public class Supplier
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string CompanyName { get; set; } = string.Empty;

    [StringLength(100)]
    public string? ContactName { get; set; }

    [StringLength(20)]
    public string? Phone { get; set; }

    [StringLength(150)]
    public string? Email { get; set; }

    [StringLength(250)]
    public string? Address { get; set; }

    [StringLength(50)]
    public string? TaxNumber { get; set; }

    public bool IsActive { get; set; } = true;
}
