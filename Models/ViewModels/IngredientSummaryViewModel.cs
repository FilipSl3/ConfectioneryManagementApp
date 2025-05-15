namespace ConfectioneryManagementApp.Models.ViewModels;

public class IngredientSummaryViewModel
{
    public string Name { get; set; }
    public string Unit { get; set; } //"g", "ml", "szt"
    public double TotalAmount { get; set; }
}