namespace ConfectioneryManagementApp.Models;

public class Order
{
    public int Id { get; set; } 
    public string ClientName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DeliveryDate { get; set; }
    public string OrderItems { get; set; }
    public decimal? TotalAmount { get; set; }
}