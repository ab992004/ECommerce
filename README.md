# ECommerce521

An e-commerce web application built with ASP.NET Core MVC (.NET 9) as a graduation project for the Eraasoft ASP.NET Core Diploma.

## Features

### Customer

* Browse products with filtering by category, brand, and price
* Product details with sub-images and color variants
* Shopping cart with quantity updates and promotion codes
* User profile and address management

### Authentication & Authorization

* Register, login, logout
* Email confirmation
* Forgot and reset password
* OTP validation
* Role-based authorization using ASP.NET Core Identity
* Roles: `SuperAdmin`, `Admin`, `Employee`, `Customer`

### Admin

* Dashboard
* Category, Brand, and Product CRUD
* Product sub-images and color variants
* User management
* Role-based permissions for admin actions

## Tech Stack

* ASP.NET Core MVC — .NET 9
* Entity Framework Core 9
* SQL Server
* ASP.NET Core Identity
* Mapster
* Bootstrap 5
* jQuery / jQuery Validation
* DataTables
* Chart.js
* Generic Repository Pattern
* Areas (Admin / Customer / Identity)

## Project Structure

```text
ECommerce521/
├── Areas/
├── DataAccess/
├── Migrations/
├── Models/
├── Repositories/
├── Utilities/
├── Validations/
├── ViewModels/
└── wwwroot/
```

## Getting Started

### Prerequisites

* .NET 9 SDK
* SQL Server
* Visual Studio 2022 or VS Code

### Clone

```bash
git clone https://github.com/ab992004/ECommerce521.git
cd ECommerce521
```

### Database

Configure your SQL Server connection string in your local configuration, then run:

```bash
dotnet ef database update
```

The application also applies migrations automatically on startup.

### Run

```bash
dotnet run
```

Or run the project using Visual Studio.

### Default Admin Account

The project includes a default SuperAdmin account for testing:

```text
Email: superadmin@eraasoft.com
Password: Admin123$
```

Change this password before deploying the project anywhere outside your local machine.

## Possible Improvements

* Full checkout and payment flow (cart currently supports add-to-cart and promo codes, without a complete checkout)
* Automated tests
* Product reviews/ratings UI (the rating field already exists on the model)

## About

Built by Abdelrahman Hossam Farag as a graduation project for the Eraasoft ASP.NET Core Diploma, 2026.
