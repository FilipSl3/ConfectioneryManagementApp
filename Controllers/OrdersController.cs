using Microsoft.AspNetCore.Mvc;
using ConfectioneryManagementApp.Models;

namespace ConfectioneryManagementApp.Controllers;

public class OrdersController : Controller
{
    public IActionResult Index()
    {
        var orders = new List<Order>
        {
            new() { ClientName = "Jan", PhoneNumber = "123456789", DeliveryDate = DateTime.Today, OrderItems = "Rogal x 5" },
            new() { ClientName = "Anna", PhoneNumber = "987654321", DeliveryDate = DateTime.Today.AddDays(1), OrderItems = "Sernik x 2" }
        };
        return View(orders);
    }
}