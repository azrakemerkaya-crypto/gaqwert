using System.ComponentModel.DataAnnotations;

namespace BordoStock.Models;

public class User
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string FullName { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required, StringLength(255)]
    public string PasswordHash { get; set; } = string.Empty;

    [StringLength(20)]
    public string? Phone { get; set; }

    [Required, StringLength(50)]
    public string Role { get; set; } = "Admin";

    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
