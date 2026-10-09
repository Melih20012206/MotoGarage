# MotoGarage

MotoGarage is an ASP.NET Core MVC application for managing motorcycle customers, their motorcycles and service history.

## Features

- Create, list, edit and delete customers. Customers with motorcycles cannot be deleted.
- Create, list, view details, edit and delete motorcycles.
- Create, list, edit and delete service records with a service type, date, price and optional notes.
- View each motorcycle's service history and total service cost.
- View dashboard counts for customers, motorcycles and service records, plus total service value.
- Prices are recorded and displayed in euros (EUR).
- Form validation and responsive pages using Bootstrap.

## Technologies

- C# and .NET 8
- ASP.NET Core MVC and Razor views
- Entity Framework Core 8.0.31
- SQL Server Express LocalDB
- Bootstrap and jQuery Validation
- Dependency injection

## Requirements for local setup

- Windows with the .NET 8 SDK installed.
- SQL Server Express LocalDB, with the `(localdb)\MSSQLLocalDB` instance available. LocalDB can be installed through Visual Studio Installer's individual components.
- Git to clone the repository.
- Optional: Visual Studio 2022 with the ASP.NET and web development workload.

## Configuration

The default connection string is in `MotoGarage/appsettings.json`:

```json
"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=MotoGarageDb;Trusted_Connection=True;MultipleActiveResultSets=true"
```

The application uses Windows authentication. No database username, password, API keys or custom environment variables are required for the default local setup. With LocalDB installed, no configuration edits are needed.

LocalDB requires Windows. A different SQL Server instance requires configuring `ConnectionStrings:DefaultConnection` for that environment.

## Run locally

Clone the branch containing the application:

```bash
git clone --branch master https://github.com/Melih20012206/MotoGarage.git
cd MotoGarage
dotnet restore MotoGarage.sln
```

Install the matching EF Core command-line tool if it is not already installed:

```bash
dotnet tool install --global dotnet-ef --version 8.0.31
```

If a different version is already installed, update it:

```bash
dotnet tool update --global dotnet-ef --version 8.0.31
```

Apply the included migrations to create the database:

```bash
dotnet ef database update --project MotoGarage/MotoGarage.csproj
```

For local HTTPS, trust the development certificate:

```bash
dotnet dev-certs https --trust
```

Start the application:

```bash
dotnet run --project MotoGarage/MotoGarage.csproj
```

Open the address printed in the terminal after `Now listening on`.

### Visual Studio alternative

1. Open `MotoGarage.sln`.
2. Allow NuGet packages to restore.
3. Open Tools → NuGet Package Manager → Package Manager Console.
4. Select MotoGarage as the default project and run `Update-Database`.
5. Set MotoGarage as the startup project and run it.

## First use

The database starts without application data. Create a customer first, then add a motorcycle belonging to that customer. Add service records by selecting an existing motorcycle.

Use Home, Customers, Motorcycles and Service History to navigate. Motorcycle details show the service records linked to that motorcycle.

Deleting a motorcycle also deletes its associated service records under the current database relationship configuration.

## Project structure

- `Controllers`: request handling for the homepage, customers, motorcycles and service records.
- `Data`: database context and entity classes. Entity classes currently use the `MotoGarage.Models` namespace.
- `Models`: view models, including the dashboard model.
- `Views`: Razor pages, shared layout and validation partial.
- `Migrations`: Entity Framework Core database migrations.
- `wwwroot`: CSS, JavaScript and front-end libraries.

## Purpose

Developed as a motorcycle garage management project for the SoftUni ASP.NET Fundamentals course, with scope for further development during ASP.NET Advanced.
