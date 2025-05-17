using ConfectioneryManagementApp.Models.Entities;

namespace ConfectioneryManagementApp.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        if (context.Ingredients.Any()) return; // już zainicjalizowane

        // SKŁADNIKI
        var sugar = new IngredientEntity { Name = "Cukier" };
        var flour = new IngredientEntity { Name = "Mąka" };
        var mascarpone = new IngredientEntity { Name = "Mascarpone" };
        var cream = new IngredientEntity { Name = "Śmietana 36%" };
        var eggs = new IngredientEntity { Name = "Jajka" };

        context.Ingredients.AddRange(sugar, flour, mascarpone, cream, eggs);

        // CIASTA
        var cheesecake = new PastryEntity
        {
            Name = "Sernik",
            Price = 60,
            PastryIngredients = new List<PastryIngredientEntity>
            {
                new() { Ingredient = sugar, Quantity = 200 },
                new() { Ingredient = eggs, Quantity = 300 },
                new() { Ingredient = cream, Quantity = 150 }
            }
        };

        var berryCake = new PastryEntity
        {
            Name = "Malinowa chmurka",
            Price = 70,
            PastryIngredients = new List<PastryIngredientEntity>
            {
                new() { Ingredient = sugar, Quantity = 150 },
                new() { Ingredient = flour, Quantity = 250 },
                new() { Ingredient = cream, Quantity = 200 }
            }
        };

        context.Pastries.AddRange(cheesecake, berryCake);

        // TORTY
        var cake16 = new CakeEntity
        {
            Flavor = "Czekoladowy",
            Size = "16 cm",
            CakeIngredients = new List<CakeIngredientEntity>
            {
                new() { Ingredient = flour, Quantity = 200 },
                new() { Ingredient = sugar, Quantity = 150 },
                new() { Ingredient = mascarpone, Quantity = 300 }
            }
        };

        var cake22 = new CakeEntity
        {
            Flavor = "Truskawkowy",
            Size = "22 cm",
            CakeIngredients = new List<CakeIngredientEntity>
            {
                new() { Ingredient = flour, Quantity = 250 },
                new() { Ingredient = sugar, Quantity = 200 },
                new() { Ingredient = cream, Quantity = 200 }
            }
        };

        context.Cakes.AddRange(cake16, cake22);

        // ZAMÓWIENIA
        var order1 = new OrderEntity
        {
            ClientName = "Jan Kowalski",
            PhoneNumber = "123456789",
            DeliveryDate = DateTime.UtcNow.AddDays(1),
            IsCompleted = false,
            Cakes = new List<PastryEntity> { cheesecake },
            OrderedCakes = new List<CakeEntity> { cake16 },
            DecorationDescription = "Delikatna dekoracja z owocami"
        };

        var order2 = new OrderEntity
        {
            ClientName = "Anna Nowak",
            PhoneNumber = "987654321",
            DeliveryDate = DateTime.UtcNow.AddDays(2),
            IsCompleted = true,
            Cakes = new List<PastryEntity> { berryCake },
            OrderedCakes = new List<CakeEntity> { cake22 },
            DecorationDescription = "Różowe róże i napis"
        };

        context.Orders.AddRange(order1, order2);
        context.SaveChanges();
    }
}
