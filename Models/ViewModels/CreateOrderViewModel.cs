using System.ComponentModel.DataAnnotations;

namespace ConfectioneryManagementApp.Models.ViewModels;

public class CreateOrderViewModel
{
    [Required]
    public string ClientName { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    public DateTime DeliveryDate { get; set; }

    public string DecorationDescription { get; set; }

    public List<int> SelectedPastryIds { get; set; } = new();
    public List<int> SelectedCakeIds { get; set; } = new();
}