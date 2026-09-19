using BordoStock.Data;
using BordoStock.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BordoStock.Controllers;

public class StockMovementsController : Controller
{
    private readonly AppDbContext _context;

    public StockMovementsController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var movements = await _context.StockMovements
            .Include(m => m.Product)
            .Include(m => m.Warehouse)
            .OrderByDescending(m => m.MovementDate)
            .ToListAsync();

        return View(movements);
    }

    public async Task<IActionResult> Create()
    {
        await LoadDropDowns();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StockMovement movement)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropDowns();
            return View(movement);
        }

        var product = await _context.Products.FindAsync(movement.ProductId);
        if (product == null)
        {
            ModelState.AddModelError("ProductId", "Geçerli ürün seçiniz.");
            await LoadDropDowns();
            return View(movement);
        }

        if (movement.MovementType == "Out")
        {
            product.StockQuantity -= movement.Quantity;
        }
        else if (movement.MovementType == "In")
        {
            product.StockQuantity += movement.Quantity;
        }

        _context.StockMovements.Add(movement);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadDropDowns()
    {
        ViewBag.Products = await _context.Products
            .Select(p => new SelectListItem
            {
                Value = p.Id.ToString(),
                Text = p.Name
            })
            .ToListAsync();

        ViewBag.Warehouses = await _context.Warehouses
            .Where(w => w.IsActive)
            .Select(w => new SelectListItem
            {
                Value = w.Id.ToString(),
                Text = w.Name
            })
            .ToListAsync();
    }
}
