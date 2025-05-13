using Microsoft.AspNetCore.Mvc;
using ConfectioneryManagementApp.Models;

namespace ConfectioneryManagementApp.Controllers;

public class CakeOrdersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}