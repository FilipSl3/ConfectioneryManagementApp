using System.ComponentModel.DataAnnotations;

namespace ConfectioneryManagementApp.Models.Entities;

public class OrderEntity
{
    public int Id { get; set; }

    [Required]
    public string ClientName { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    public DateTime DeliveryDate { get; set; }

    public bool IsCompleted { get; set; }

    public List<PastryEntity> Cakes { get; set; } = new();
    public List<CakeEntity> OrderedCakes { get; set; } = new();
    public string? DecorationDescription { get; set; }

}