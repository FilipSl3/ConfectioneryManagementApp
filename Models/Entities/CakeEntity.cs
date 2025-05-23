namespace ConfectioneryManagementApp.Models.Entities;

public class CakeEntity
{
    public int Id { get; set; }
    public string Flavor { get; set; } = string.Empty;
    public string Size { get; set; } = string.Empty;
    public ICollection<CakeIngredientEntity> CakeIngredients { get; set; } = new List<CakeIngredientEntity>();
}