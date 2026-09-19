using BordoStock.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BordoStock.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly AppDbContext _context;
    public HomeController(AppDbContext context) => _context = context;

    public async Task<IActionResult> Index()
    {
        ViewBag.TotalProducts = await _context.Products.CountAsync(p => p.IsActive);
        ViewBag.TotalStock = await _context.Products.SumAsync(p => (int?)p.StockQuantity) ?? 0;
        ViewBag.LowStockProducts = await _context.Products.CountAsync(p => p.IsActive && p.StockQuantity <= p.MinStockLevel);
        ViewBag.StockValue = await _context.Products.SumAsync(p => (decimal?)(p.StockQuantity * p.PurchasePrice)) ?? 0;
        var movements = await _context.StockMovements.Include(m => m.Product).Include(m => m.Warehouse)
            .OrderByDescending(m => m.MovementDate).Take(8).ToListAsync();
        return View(movements);
    }
}
