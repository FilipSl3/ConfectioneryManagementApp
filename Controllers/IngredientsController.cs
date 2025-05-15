using Microsoft.AspNetCore.Mvc;
using ConfectioneryManagementApp.Models.ViewModels;

namespace ConfectioneryManagementApp.Controllers;

public class IngredientsController : Controller
{
    public IActionResult Index(DateTime? startDate, DateTime? endDate)
    {
        var ingredients = new List<IngredientSummaryViewModel>
        {
            new() { Name = "Mąka pszenna", Unit = "kg", TotalAmount = 12.5 },
            new() { Name = "Cukier", Unit = "kg", TotalAmount = 4.0 },
            new() { Name = "Serek mascarpone", Unit = "kg", TotalAmount = 2.3 },
            new() { Name = "Jajka", Unit = "szt", TotalAmount = 36 },
        };

        return View(ingredients);
    }
}