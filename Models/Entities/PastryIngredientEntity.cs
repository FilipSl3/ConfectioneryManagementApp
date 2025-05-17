namespace ConfectioneryManagementApp.Models.Entities;

public class PastryIngredientEntity
{
    public int PastryId { get; set; }
    public PastryEntity Pastry { get; set; } = null!;

    public int IngredientId { get; set; }
    public IngredientEntity Ingredient { get; set; } = null!;

    public float Quantity { get; set; }
}