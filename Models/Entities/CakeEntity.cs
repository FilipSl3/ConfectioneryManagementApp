namespace ConfectioneryManagementApp.Models.Entities;

public class CakeEntity
{
    public int Id { get; set; }
    public string Flavor { get; set; }
    public int Quantity { get; set; }

    public int OrderEntityId { get; set; }
    public OrderEntity Order { get; set; }
}