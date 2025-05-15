using Microsoft.AspNetCore.Mvc;
using ConfectioneryManagementApp.Models.Entities;
using ConfectioneryManagementApp.Models.ViewModels;

namespace ConfectioneryManagementApp.Controllers;

public class CakesController : Controller
{
    public static readonly List<OrderEntity> Orders = new()
    {
        new OrderEntity
        {
            ClientName = "Jan",
            DeliveryDate = DateTime.Today,
            Cakes = new List<CakeEntity>
            {
                new CakeEntity { Flavor = "Sernik", Quantity = 2 },
                new CakeEntity { Flavor = "Makowiec", Quantity = 1 },
                new CakeEntity { Flavor = "Ażurowiec", Quantity = 5 },
                new CakeEntity { Flavor = "Makowiec", Quantity = 1 }
            }
        },
        new OrderEntity
        {
            ClientName = "Anna",
            DeliveryDate = DateTime.Today.AddDays(1),
            Cakes = new List<CakeEntity>
            {
                new CakeEntity { Flavor = "Sernik", Quantity = 3 }
            }
        }
    };

    public IActionResult Index(DateTime? from, DateTime? to)
    {
        var filtered = Orders.AsEnumerable();

        if (from.HasValue)
            filtered = filtered.Where(o => o.DeliveryDate >= from.Value);
        if (to.HasValue)
            filtered = filtered.Where(o => o.DeliveryDate <= to.Value);

        var cakes = filtered
            .SelectMany(o => o.Cakes)
            .GroupBy(c => c.Flavor)
            .Select(g => new CakeSummaryViewModel
            {
                Name = g.Key,
                TotalQuantity = g.Sum(c => c.Quantity)
            })
            .Where(c => c.TotalQuantity > 0)
            .OrderBy(c => c.Name)
            .ToList();

        return View(cakes);
    }
}