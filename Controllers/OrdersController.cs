using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConfectioneryManagementApp.Data;
using ConfectioneryManagementApp.Models.Entities;
using ConfectioneryManagementApp.Models.ViewModels;

namespace ConfectioneryManagementApp.Controllers;

public class OrdersController : Controller
{
    private readonly AppDbContext _context;

    public OrdersController(AppDbContext context)
    {
        _context = context;
    }

    private async Task SetCakeAndPastryViewBags()
    {
        ViewBag.Pastries = await _context.Pastries.ToListAsync();
        ViewBag.Cakes = await _context.Cakes.ToListAsync();
    }



    public async Task<IActionResult> Index(DateTime? from, DateTime? to, string? search)
    {
        var query = _context.Orders
            .Include(o => o.Cakes)
            .Include(o => o.OrderedCakes)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(o => EF.Functions.ILike(o.ClientName, $"%{search}%"));
        
        if (!from.HasValue)
            from = DateTime.Today;
        if (!to.HasValue)
            to = DateTime.Today.AddDays(7);

        if (from.HasValue)
        {
            var fromUtc = DateTime.SpecifyKind(from.Value, DateTimeKind.Utc);
            query = query.Where(o => o.DeliveryDate >= fromUtc);
        }

        if (to.HasValue)
        {
            var toUtc = DateTime.SpecifyKind(to.Value, DateTimeKind.Utc);
            query = query.Where(o => o.DeliveryDate <= toUtc);
        }

        var orders = await query
            .Select(o => new OrderViewModel
            {
                Id = o.Id,
                ClientName = o.ClientName,
                PhoneNumber = o.PhoneNumber,
                DeliveryDate = o.DeliveryDate,
                IsCompleted = o.IsCompleted,
                Pastries = o.Cakes.Select(c => c.Name).ToList(),
                Cakes = o.OrderedCakes.Select(c => $"{c.Flavor} {c.Size}").ToList()
            })
            .ToListAsync();

        ViewBag.From = from?.ToString("yyyy-MM-dd");
        ViewBag.To = to?.ToString("yyyy-MM-dd");
        
        return View(orders);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await SetCakeAndPastryViewBags();
        return View(new CreateOrderViewModel
        {
            DeliveryDate = DateTime.Today
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateOrderViewModel model)
    {
        
        await SetCakeAndPastryViewBags();
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var selectedPastries = await _context.Pastries.Where(p => model.SelectedPastryIds.Contains(p.Id)).ToListAsync();
        var selectedCakes = await _context.Cakes.Where(c => model.SelectedCakeIds.Contains(c.Id)).ToListAsync();

        var order = new OrderEntity
        {
            ClientName = model.ClientName,
            PhoneNumber = model.PhoneNumber,
            DeliveryDate = DateTime.SpecifyKind(model.DeliveryDate, DateTimeKind.Utc),
            IsCompleted = false,
            Cakes = selectedPastries,
            OrderedCakes = selectedCakes,
            DecorationDescription = model.DecorationDescription
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        
        if (id <= 0) return BadRequest();

        var order = await _context.Orders
            .Include(o => o.Cakes)
            .Include(o => o.OrderedCakes)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null)
            return NotFound();

        var model = new CreateOrderViewModel
        {
            Id = order.Id,
            ClientName = order.ClientName,
            PhoneNumber = order.PhoneNumber,
            DeliveryDate = order.DeliveryDate.ToLocalTime(),
            DecorationDescription = order.DecorationDescription,
            SelectedPastryIds = order.Cakes.Select(c => c.Id).ToList(),
            SelectedCakeIds = order.OrderedCakes.Select(c => c.Id).ToList()
        };

        await SetCakeAndPastryViewBags();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(CreateOrderViewModel model)
    {
        await SetCakeAndPastryViewBags();
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var order = await _context.Orders
            .Include(o => o.Cakes)
            .Include(o => o.OrderedCakes)
            .FirstOrDefaultAsync(o => o.Id == model.Id);

        if (order == null)
            return NotFound();

        order.ClientName = model.ClientName;
        order.PhoneNumber = model.PhoneNumber;
        order.DeliveryDate = DateTime.SpecifyKind(model.DeliveryDate, DateTimeKind.Utc);
        order.DecorationDescription = model.DecorationDescription;

        order.Cakes = await _context.Pastries
            .Where(p => model.SelectedPastryIds.Contains(p.Id)).ToListAsync();

        order.OrderedCakes = await _context.Cakes
            .Where(c => model.SelectedCakeIds.Contains(c.Id)).ToListAsync();

        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }

    
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
            return NotFound("Zamówienie nie istnieje.");

        try
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return Ok(); // Status 200
        }
        catch (DbUpdateException ex)
        {
            // Można zalogować: _logger.LogError(ex, "Błąd usuwania zamówienia {Id}", id);
            return StatusCode(500, "Nie można usunąć zamówienia – może być powiązane z innymi danymi.");
        }
    }

}
