# E-Commerce Web Application (ASP.NET Core MVC)

A full-stack e-commerce web application built with ASP.NET Core MVC and Entity Framework Core, developed as part of the EraaSoft .NET Backend Development Diploma. The app is structured into separate Admin, Customer, and Identity areas, with role-based access control, product management, and a shopping cart flow.

## Features

**Customer**
- Browse products with filtering by category, brand, price range, and promotions
- Product details page with a main image and multiple sub-images
- Add products to a shopping cart and manage quantities
- Register, log in, and reset password via OTP-based email verification
- Personal profile management

**Admin**
- Full CRUD management for Products, Categories, and Brands
- Multi-image upload per product (main image + sub-images)
- Role-based access with three permission levels: Super Admin, Admin, and Employee
- User management dashboard

## Tech Stack

- **Framework:** ASP.NET Core MVC (.NET 9)
- **ORM:** Entity Framework Core (Code-First, migrations)
- **Database:** SQL Server
- **Authentication:** ASP.NET Core Identity with role-based authorization
- **Architecture:** Repository Pattern with dependency injection, organized by Areas (Admin / Customer / Identity)
- **Frontend:** Razor Views, Bootstrap

## Architecture

The project follows a layered structure:
- `Areas/` — feature areas (Admin, Customer, Identity), each with its own controllers and views
- `Repositories/` — a generic repository pattern (`IRepository<T>` / `Repository<T>`) abstracting data access from EF Core
- `Models/` — domain entities (Product, Category, Brand, Cart, Promotion, ApplicationUser, etc.)
- `ViewModels/` — view-specific models decoupled from domain entities
- `DataAccess/` — `ApplicationDbContext` and Fluent API entity configurations
- `Migrations/` — incremental EF Core migrations tracking schema evolution

## Getting Started

### Prerequisites
- .NET 9 SDK
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 (recommended) or VS Code

### Setup
1. Clone the repository
   ```
   git clone https://github.com/ab992004/ECommerce521.git
   ```
2. Update the connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ECommerce521DB;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```
3. Apply migrations from the Package Manager Console:
   ```
   Update-Database
   ```
4. Run the project (F5 in Visual Studio, or `dotnet run`)

## Roadmap

- [ ] Order & checkout flow (currently cart-only)
- [ ] Order history for customers and order management for admins
- [ ] Product reviews and ratings
- [ ] Unit tests (xUnit / Moq)

## Notes

This project was built as a learning project during the EraaSoft .NET Backend Development Diploma to practice Clean Architecture concepts, the Repository Pattern, and ASP.NET Core Identity in a realistic, full-featured application.
