using Microsoft.AspNetCore.Mvc;
using ConfectioneryManagementApp.Models;

namespace ConfectioneryManagementApp.Controllers;

public class IngredientsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}