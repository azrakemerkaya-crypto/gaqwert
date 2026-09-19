using System.Security.Claims;
using BordoStock.Data;
using BordoStock.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BordoStock.Controllers;

[Authorize]
public class StockMovementsController : Controller
{
    private readonly AppDbContext _context;
    public StockMovementsController(AppDbContext context) => _context = context;

    public async Task<IActionResult> Index() => View(await _context.StockMovements.Include(m => m.Product).Include(m => m.Warehouse).OrderByDescending(m => m.MovementDate).ToListAsync());

    public async Task<IActionResult> Create()
    {
        await LoadDropDowns();
        return View(new StockMovement { MovementDate = DateTime.Now });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(StockMovement movement)
    {
        if (movement.Quantity <= 0) ModelState.AddModelError(nameof(movement.Quantity), "Miktar sıfırdan büyük olmalıdır.");
        var product = await _context.Products.FindAsync(movement.ProductId);
        if (product == null) ModelState.AddModelError(nameof(movement.ProductId), "Ürün bulunamadı.");
        if (movement.MovementType == "Out" && product != null && product.StockQuantity < movement.Quantity)
            ModelState.AddModelError(nameof(movement.Quantity), "Çıkış miktarı mevcut stoktan fazla olamaz.");
        if (!ModelState.IsValid) { await LoadDropDowns(); return View(movement); }

        if (movement.MovementType == "Out") product!.StockQuantity -= movement.Quantity;
        else product!.StockQuantity += movement.Quantity;
        movement.UnitPrice = movement.UnitPrice == 0 ? product.PurchasePrice : movement.UnitPrice;
        _context.StockMovements.Add(movement);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadDropDowns()
    {
        ViewBag.Products = await _context.Products.Where(p => p.IsActive).Select(p => new SelectListItem(p.Name, p.Id.ToString())).ToListAsync();
        ViewBag.Warehouses = await _context.Warehouses.Where(w => w.IsActive).Select(w => new SelectListItem(w.Name, w.Id.ToString())).ToListAsync();
    }
}
