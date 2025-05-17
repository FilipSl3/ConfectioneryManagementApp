namespace ConfectioneryManagementApp.Models.ViewModels;

public class OrderViewModel
{
    public string ClientName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DeliveryDate { get; set; }
    public bool IsCompleted { get; set; }

    public List<string> Pastries { get; set; } = new();
    public List<string> Cakes { get; set; } = new();
}