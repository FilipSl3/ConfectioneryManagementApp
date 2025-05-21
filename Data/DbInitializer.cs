using ConfectioneryManagementApp.Models.Entities;

namespace ConfectioneryManagementApp.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        if (context.Ingredients.Any()) return;

        // SKŁADNIKI
        var sugar = new IngredientEntity { Name = "Cukier" };
        var flour = new IngredientEntity { Name = "Mąka" };
        var mascarpone = new IngredientEntity { Name = "Mascarpone" };
        var cream = new IngredientEntity { Name = "Śmietana 36%" };
        var eggs = new IngredientEntity { Name = "Jajka" };
        var apples = new IngredientEntity { Name = "Jabłka" };
        var chocolate = new IngredientEntity { Name = "Czekolada" };
        var vanilla = new IngredientEntity { Name = "Wanilia" };
        var oreo = new IngredientEntity { Name = "Oreo" };

        context.Ingredients.AddRange(sugar, flour, mascarpone, cream, eggs, apples, chocolate, vanilla, oreo);

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

        var applePie = new PastryEntity
        {
            Name = "Szarlotka",
            Price = 50,
            PastryIngredients = new List<PastryIngredientEntity>
            {
                new() { Ingredient = flour, Quantity = 200 },
                new() { Ingredient = apples, Quantity = 300 },
                new() { Ingredient = sugar, Quantity = 100 }
            }
        };

        var brownie = new PastryEntity
        {
            Name = "Brownie",
            Price = 65,
            PastryIngredients = new List<PastryIngredientEntity>
            {
                new() { Ingredient = chocolate, Quantity = 300 },
                new() { Ingredient = sugar, Quantity = 150 },
                new() { Ingredient = eggs, Quantity = 200 }
            }
        };

        context.Pastries.AddRange(cheesecake, berryCake, applePie, brownie);

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

        var cake28 = new CakeEntity
        {
            Flavor = "Waniliowy",
            Size = "28 cm",
            CakeIngredients = new List<CakeIngredientEntity>
            {
                new() { Ingredient = flour, Quantity = 300 },
                new() { Ingredient = sugar, Quantity = 250 },
                new() { Ingredient = vanilla, Quantity = 50 }
            }
        };

        var cakeOreo = new CakeEntity
        {
            Flavor = "Oreo",
            Size = "16 cm",
            CakeIngredients = new List<CakeIngredientEntity>
            {
                new() { Ingredient = flour, Quantity = 200 },
                new() { Ingredient = sugar, Quantity = 100 },
                new() { Ingredient = oreo, Quantity = 150 }
            }
        };

        context.Cakes.AddRange(cake16, cake22, cake28, cakeOreo);

        // ZAMÓWIENIA
        var order1 = new OrderEntity
        {
            ClientName = "Jan Kowalski",
            PhoneNumber = "123456789",
            DeliveryDate = DateTime.SpecifyKind(new DateTime(2025, 5, 20), DateTimeKind.Utc),

            IsCompleted = false,
            Cakes = new List<PastryEntity> { cheesecake },
            OrderedCakes = new List<CakeEntity> { cake16 },
            DecorationDescription = "Delikatna dekoracja z owocami"
        };

        var order2 = new OrderEntity
        {
            ClientName = "Anna Nowak",
            PhoneNumber = "987654321",
            DeliveryDate = DateTime.SpecifyKind(new DateTime(2025, 5, 25), DateTimeKind.Utc),
            IsCompleted = true,
            Cakes = new List<PastryEntity> { berryCake },
            OrderedCakes = new List<CakeEntity> { cake22 },
            DecorationDescription = "Różowe róże i napis"
        };

        var order3 = new OrderEntity
        {
            ClientName = "Tomasz Lis",
            PhoneNumber = "555666777",
            DeliveryDate = DateTime.SpecifyKind(new DateTime(2025, 5, 30), DateTimeKind.Utc),
            IsCompleted = false,
            Cakes = new List<PastryEntity> { applePie },
            OrderedCakes = new List<CakeEntity> { cake28 },
            DecorationDescription = "Złote litery"
        };

        var order4 = new OrderEntity
        {
            ClientName = "Katarzyna Wiśniewska",
            PhoneNumber = "444555666",
            DeliveryDate = DateTime.SpecifyKind(new DateTime(2025, 5, 27), DateTimeKind.Utc),
            IsCompleted = false,
            Cakes = new List<PastryEntity> { brownie },
            OrderedCakes = new List<CakeEntity> { cakeOreo },
            DecorationDescription = "Minimalistyczna dekoracja Oreo"
        };

        var order5 = new OrderEntity
        {
            ClientName = "Michał Nowicki",
            PhoneNumber = "111222333",
            DeliveryDate = DateTime.SpecifyKind(new DateTime(2025, 6, 05), DateTimeKind.Utc),
            IsCompleted = true,
            Cakes = new List<PastryEntity> { cheesecake, berryCake },
            OrderedCakes = new List<CakeEntity> { cake16, cake22 },
            DecorationDescription = "Dwa piętra, bez owoców"
        };

        context.Orders.AddRange(order1, order2, order3, order4, order5);
        context.SaveChanges();
    }
}
