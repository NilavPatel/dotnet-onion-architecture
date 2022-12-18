# Commands

## Run below commands to run backend project
````
dotnet run --project=src/MyApp.WebApi/MyApp.WebApi.csproj

dotnet build --project=src/MyApp.WebApi/MyApp.WebApi.csproj

dotnet clean

dotnet restore

dotnet test
````

## Run below commands in sequence to create architecture for backend
````
mkdir ERP
cd ERP
dotnet new sln -n=ERP
mkdir src

// Domain
dotnet new classlib -o src/MyApp.Domain
dotnet sln add src/MyApp.Domain/MyApp.Domain.csproj

// Application
dotnet new classlib -o src/MyApp.Application
dotnet sln add src/MyApp.Application/MyApp.Application.csproj
dotnet add src/MyApp.Application/MyApp.Application.csproj reference src/MyApp.Domain/MyApp.Domain.csproj

// Infrastructure
dotnet new classlib -o src/MyApp.Infrastructure
dotnet sln add src/MyApp.Infrastructure/MyApp.Infrastructure.csproj
dotnet add src/MyApp.Infrastructure/MyApp.Infrastructure.csproj reference src/MyApp.Domain/MyApp.Domain.csproj
dotnet add src/MyApp.Infrastructure/MyApp.Infrastructure.csproj reference src/MyApp.Application/Application.Domain.csproj

// DbMigrations
dotnet new classlib -o src/MyApp.DbMigrations
dotnet sln add src/MyApp.DbMigrations/MyApp.DbMigrations.csproj
dotnet add src/MyApp.DbMigrations/MyApp.DbMigrations.csproj reference src/MyApp.Infrastructure/MyApp.Infrastructure.csproj

// WebApi
dotnet new webapi -o src/MyApp.WebApi
dotnet sln add src/MyApp.WebApi/MyApp.WebApi.csproj
dotnet add src/MyApp.WebApi/MyApp.WebApi.csproj reference src/MyApp.Domain/MyApp.Domain.csproj
dotnet add src/MyApp.WebApi/MyApp.WebApi.csproj reference src/MyApp.Application/MyApp.Application.csproj
dotnet add src/MyApp.WebApi/MyApp.WebApi.csproj reference src/MyApp.Infrastructure/MyApp.Infrastructure.csproj
dotnet add src/MyApp.WebApi/MyApp.WebApi.csproj reference src/MyApp.DbMigrations/MyApp.DbMigrations.csproj

// Test-Domain
dotnet new xunit -o src/Tests/MyApp.Domain.Test
dotnet sln add src/Tests/MyApp.Domain.Test
dotnet add src/Tests/MyApp.Domain.Test reference src/MyApp.Domain
dotnet add src/Tests/MyApp.Domain.Test reference src/MyApp.Infrastructure

// Test-Application
dotnet new xunit -o src/Tests/MyApp.Application.Test
dotnet sln add src/Tests/MyApp.Application.Test
dotnet add src/Tests/MyApp.Application.Test reference src/MyApp.Application

// Test-Infrastructure
dotnet new xunit -o src/Tests/MyApp.Infrastructure.Test
dotnet sln add src/Tests/MyApp.Infrastructure.Test
dotnet add src/Tests/MyApp.Infrastructure.Test reference src/MyApp.Infrastructure
````

## Commands to Update Db Migrations
In MyApp.DbMigrations Project run below commands
````
// Install tool
dotnet tool install --global dotnet-ef

// Use this command to create new migration
dotnet ef migrations add {migration-name} --startup-project ../MyApp.WebApi/MyApp.WebApi.csproj
dotnet ef database update {migration-name} --startup-project ../MyApp.WebApi/MyApp.WebApi.csproj // Revert back to specific migration

// Use this command to update database
dotnet ef database update

// Use this command to remove last migration
dotnet ef migrations remove
````