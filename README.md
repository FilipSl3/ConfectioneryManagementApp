# Confectionery Management App

Aplikacja webowa do zarządzania zamówieniami w cukierni, stworzona w technologii ASP.NET Core MVC z wykorzystaniem Entity Framework Core i bazy danych PostgreSQL.

## 📦 Funkcje aplikacji

- 📋 Lista zamówień z filtrowaniem po dacie i nazwisku klienta
- 🍰 Widok tortów i ciast z podsumowaniem zamówionych ilości
- 🧾 Widok zapotrzebowania na składniki na podstawie złożonych zamówień
- ➕ Formularz dodawania nowego zamówienia oraz edycji istniejacych zamówień
- ❌ Usuwanie zamówień za pomocą AJAX (z potwierdzeniem)
- ✅ Walidacja kontekstowa — np. pole „Opis dekoracji” wymagane tylko, gdy dodano tort
- 📅 Domyślny filtr dat: od dziś do 7 dni w przód
- 🔍 Wyszukiwanie po imieniu i nazwisku klienta
- ⚙️ Serwerowa obsługa filtrowania i sortowania
- 🌐 Frontend oparty na Bootstrap 5

## 🧪 Technologie

- ASP.NET Core 9.0
- Entity Framework Core 9.0.5
- Npgsql (obsługa PostgreSQL)
- Bootstrap 5
- jQuery (AJAX)
- PostgreSQL

## ▶️ Uruchamianie aplikacji

1. Zainstaluj .NET 9 SDK
2. Skonfiguruj połączenie do bazy PostgreSQL w pliku `appsettings.json`
3. Wykonaj migrację bazy danych:

```bash
dotnet ef database update
```

4. Uruchom aplikację:

```bash
dotnet run
```

## 📁 Struktura projektu

- `Controllers/` — kontrolery MVC (np. OrdersController)
- `Views/` — widoki Razor i częściowe
- `Models/Entities/` — klasy encji bazodanowych
- `Models/ViewModels/` — modele widoków
- `wwwroot/` — zasoby frontendowe (JS, CSS)
- `Data/AppDbContext.cs` — kontekst bazy danych EF Core

## 🗃️ Struktura bazy danych

- **Orders** – zamówienia klientów
- **Cakes** – lista dostępnych tortów
- **Pastries** – lista dostępnych ciast
- **Ingredients** – składniki wykorzystywane w produkcji
- **CakeIngredientEntity / PastryIngredientEntity** – relacje N:M między ciastami/tortami a składnikami
- **OrderCakes / OrderPastries** – relacje N:M między zamówieniami a produktami

## 🔗 Relacje

- CakeEntity ⬌ IngredientEntity (N:M)
- PastryEntity ⬌ IngredientEntity (N:M)
- OrderEntity ⬌ CakeEntity (N:M)
- OrderEntity ⬌ PastryEntity (N:M)

