using Microsoft.AspNetCore.Mvc;
using ConfectioneryManagementApp.Models.ViewModels;

namespace ConfectioneryManagementApp.Controllers;

public class CakeOrdersController : Controller
{
    public IActionResult Index(DateTime? startDate, DateTime? endDate)
    {
        var cakeOrders = new List<CakeOrderViewModel>
        {
            new()
            {
                Flavor = "Truskawkowy",
                Size = "Duży",
                DeliveryDate = DateTime.Today,
                ClientName = "Jan Kowalski",
                DecorationDescription = "Róże z kremu"
            },
            new()
            {
                Flavor = "Czekoladowy",
                Size = "Średni",
                DeliveryDate = DateTime.Today.AddDays(1),
                ClientName = "Anna Nowak",
                DecorationDescription = "Napis: 18 lat!"
            },
            new()
            {
                Flavor = "Waniliowy",
                Size = "Mały",
                DeliveryDate = DateTime.Today.AddDays(2),
                ClientName = "Katarzyna Wiśniewska",
                DecorationDescription = "Figurka misia"
            }
        };

        if (startDate.HasValue)
            cakeOrders = cakeOrders.Where(c => c.DeliveryDate >= startDate.Value).ToList();

        if (endDate.HasValue)
            cakeOrders = cakeOrders.Where(c => c.DeliveryDate <= endDate.Value).ToList();

        return View(cakeOrders.OrderBy(c => c.Flavor).ToList());
    }
}