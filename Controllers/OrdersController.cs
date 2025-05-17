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

    public async Task<IActionResult> Index()
    {
        var orders = await _context.Orders
            .Include(o => o.Cakes)
            .Include(o => o.OrderedCakes)
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
}