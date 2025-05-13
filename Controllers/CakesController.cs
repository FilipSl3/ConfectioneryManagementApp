using Microsoft.AspNetCore.Mvc;
using ConfectioneryManagementApp.Models;

namespace ConfectioneryManagementApp.Controllers;

public class CakesController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}