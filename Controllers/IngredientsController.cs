using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ConfectioneryManagementApp.Data;
using ConfectioneryManagementApp.Models.ViewModels;

namespace ConfectioneryManagementApp.Controllers;

public class IngredientsController : Controller
{
    private readonly AppDbContext _context;

    public IngredientsController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(DateTime? from, DateTime? to)
    {
        var query = _context.Orders
            .Include(o => o.Cakes).ThenInclude(p => p.PastryIngredients).ThenInclude(pi => pi.Ingredient)
            .Include(o => o.OrderedCakes).ThenInclude(c => c.CakeIngredients).ThenInclude(ci => ci.Ingredient)
            .AsQueryable();

        if (from.HasValue)
            query = query.Where(o => o.DeliveryDate >= from.Value);

        if (to.HasValue)
            query = query.Where(o => o.DeliveryDate <= to.Value);

        var orders = await query.ToListAsync();

        // składniki z ciast
        var pastryIngredients = orders
            .SelectMany(o => o.Cakes)
            .SelectMany(p => p.PastryIngredients)
            .GroupBy(i => i.Ingredient.Name)
            .Select(g => new IngredientSummaryViewModel
            {
                Name = g.Key,
                TotalQuantity = g.Sum(x => x.Quantity)
            });

        // składniki z tortów
        var cakeIngredients = orders
            .SelectMany(o => o.OrderedCakes)
            .SelectMany(c => c.CakeIngredients)
            .GroupBy(i => i.Ingredient.Name)
            .Select(g => new IngredientSummaryViewModel
            {
                Name = g.Key,
                TotalQuantity = g.Sum(x => x.Quantity)
            });

        // połączenie i sumowanie składników
        var summary = pastryIngredients
            .Concat(cakeIngredients)
            .GroupBy(i => i.Name)
            .Select(g => new IngredientSummaryViewModel
            {
                Name = g.Key,
                TotalQuantity = g.Sum(x => x.TotalQuantity)
            })
            .OrderBy(i => i.Name)
            .ToList();

        return View(summary);
    }
}
