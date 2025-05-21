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

    public async Task<IActionResult> Index(DateTime? from, DateTime? to)
    {
        var query = _context.Orders
            .Include(o => o.Cakes)
            .Include(o => o.OrderedCakes)
            .AsQueryable();

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
                ClientName = o.ClientName,
                PhoneNumber = o.PhoneNumber,
                DeliveryDate = o.DeliveryDate,
                IsCompleted = o.IsCompleted,
                Pastries = o.Cakes.Select(c => c.Name).ToList(),
                Cakes = o.OrderedCakes.Select(c => $"{c.Flavor} {c.Size}").ToList()
            })
            .ToListAsync();

        return View(orders);
    }

    
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.Pastries = await _context.Pastries.ToListAsync();
        ViewBag.Cakes = await _context.Cakes.ToListAsync();
        return PartialView("_CreateOrderPartial");
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Pastries = await _context.Pastries.ToListAsync();
            ViewBag.Cakes = await _context.Cakes.ToListAsync();
            return PartialView("_CreateOrderPartial", model);
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
}