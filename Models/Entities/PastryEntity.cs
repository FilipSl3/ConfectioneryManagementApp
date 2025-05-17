namespace ConfectioneryManagementApp.Models.Entities;

public class PastryEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public ICollection<PastryIngredientEntity> PastryIngredients { get; set; } = new List<PastryIngredientEntity>();
}
