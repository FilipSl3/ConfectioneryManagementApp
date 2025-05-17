namespace ConfectioneryManagementApp.Models.Entities;

public class IngredientEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<PastryIngredientEntity> PastryIngredients { get; set; } = new List<PastryIngredientEntity>();
    public ICollection<CakeIngredientEntity> CakeIngredients { get; set; } = new List<CakeIngredientEntity>();
}