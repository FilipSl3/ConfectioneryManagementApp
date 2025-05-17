namespace ConfectioneryManagementApp.Models.Entities;

public class CakeIngredientEntity
{
    public int CakeId { get; set; }
    public CakeEntity Cake { get; set; } = null!;

    public int IngredientId { get; set; }
    public IngredientEntity Ingredient { get; set; } = null!;

    public float Quantity { get; set; }
}