using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BordoStock.Data;
using BordoStock.Models;

namespace BordoStock.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var totalProducts = await _context.Products.CountAsync();
        var totalStock = await _context.Products.SumAsync(p => (int?)p.StockQuantity) ?? 0;
        var lowStockProducts = await _context.Products.CountAsync(p => p.StockQuantity <= p.MinStockLevel);

        var recentMovements = await _context.StockMovements
            .OrderByDescending(m => m.MovementDate)
            .Take(5)
            .Include(m => m.Product)
            .ToListAsync();

        ViewBag.TotalProducts = totalProducts;
        ViewBag.TotalStock = totalStock;
        ViewBag.LowStockProducts = lowStockProducts;

        return View(recentMovements);
    }
}
