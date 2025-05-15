using Microsoft.AspNetCore.Mvc;
using ConfectioneryManagementApp.Models.ViewModels;

namespace ConfectioneryManagementApp.Controllers;

public class OrdersController : Controller
{
    public IActionResult Index()
    {
        var orders = new List<OrderViewModel>
        {
            new()
            {
                Id = 1,
                ClientName = "Jan",
                PhoneNumber = "123456789",
                DeliveryDate = DateTime.Today,
                OrderItems = "Rogal x 5",
                TotalAmount = 2137
            },
            new()
            {
                Id = 2,
                ClientName = "Anna",
                PhoneNumber = "987654321",
                DeliveryDate = DateTime.Today.AddDays(1),
                OrderItems = "Sernik x 2",
                TotalAmount = 7312
            }
        };

        return View(orders);
    }
}