using System.ComponentModel.DataAnnotations;

namespace ConfectioneryManagementApp.Models.ViewModels;

public class CreateOrderViewModel : IValidatableObject
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Imię i nazwisko jest wymagane.")]
    public string ClientName { get; set; }
    [Required(ErrorMessage = "Numer telefonu jest wymagany.")]
    [RegularExpression(@"^\d{9}$", ErrorMessage = "Numer telefonu musi zawierać dokładnie 9 cyfr.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Data odbioru jest wymagana.")]
    public DateTime DeliveryDate { get; set; }

    public string? DecorationDescription { get; set; }

    public List<int> SelectedPastryIds { get; set; } = new();
    public List<int> SelectedCakeIds { get; set; } = new();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (SelectedCakeIds.Any() && string.IsNullOrWhiteSpace(DecorationDescription))
        {
            yield return new ValidationResult(
                "Wymagana dekoracja do zamówionego torta.",
                new[] { nameof(DecorationDescription) }
            );
        }
    }
}