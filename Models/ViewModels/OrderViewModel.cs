namespace ConfectioneryManagementApp.Models.ViewModels;

public class OrderViewModel
{
    public int Id { get; set; }
    public string ClientName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DeliveryDate { get; set; }

    public string OrderItems { get; set; } // opis pozycji np. "Rogal x 5"
    public decimal? TotalAmount { get; set; } // wartość zamówienia
}