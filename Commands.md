# Commands

## Run below commands to run backend project
````
dotnet run src/MyApp.WebApi/MyApp.WebApi.csproj

dotnet build src/MyApp.WebApi/MyApp.WebApi.csproj

dotnet publish src/MyApp.WebApi/MyApp.WebApi.csproj

dotnet clean

dotnet restore

dotnet test
````

## Run below commands in sequence to create architecture for backend
````
mkdir MyApp
cd MyApp
dotnet new sln -n=MyApp
mkdir src

// Domain
dotnet new classlib --output src/MyApp.Domain
dotnet sln add src/MyApp.Domain/MyApp.Domain.csproj

// Application
dotnet new classlib --output src/MyApp.Application
dotnet sln add src/MyApp.Application/MyApp.Application.csproj
dotnet add src/MyApp.Application/MyApp.Application.csproj reference src/MyApp.Domain/MyApp.Domain.csproj

// Infrastructure
dotnet new classlib --output src/MyApp.Infrastructure
dotnet sln add src/MyApp.Infrastructure/MyApp.Infrastructure.csproj
dotnet add src/MyApp.Infrastructure/MyApp.Infrastructure.csproj reference src/MyApp.Domain/MyApp.Domain.csproj
dotnet add src/MyApp.Infrastructure/MyApp.Infrastructure.csproj reference src/MyApp.Application/Application.Domain.csproj

// WebApi
dotnet new webapi --output src/MyApp.WebApi
dotnet sln add src/MyApp.WebApi/MyApp.WebApi.csproj
dotnet add src/MyApp.WebApi/MyApp.WebApi.csproj reference src/MyApp.Domain/MyApp.Domain.csproj
dotnet add src/MyApp.WebApi/MyApp.WebApi.csproj reference src/MyApp.Application/MyApp.Application.csproj
dotnet add src/MyApp.WebApi/MyApp.WebApi.csproj reference src/MyApp.Infrastructure/MyApp.Infrastructure.csproj

// Test-Domain
dotnet new xunit --output src/Tests/MyApp.Domain.Test
dotnet sln add src/Tests/MyApp.Domain.Test
dotnet add src/Tests/MyApp.Domain.Test reference src/MyApp.Domain
dotnet add src/Tests/MyApp.Domain.Test reference src/MyApp.Infrastructure

// Test-Application
dotnet new xunit --output src/Tests/MyApp.Application.Test
dotnet sln add src/Tests/MyApp.Application.Test
dotnet add src/Tests/MyApp.Application.Test reference src/MyApp.Application

// Test-Infrastructure
dotnet new xunit --output src/Tests/MyApp.Infrastructure.Test
dotnet sln add src/Tests/MyApp.Infrastructure.Test
dotnet add src/Tests/MyApp.Infrastructure.Test reference src/MyApp.Infrastructure
````

## Commands for Db Migration
Install EF tool
```
dotnet tool install --global dotnet-ef
```

In MyApp.Infrastructure Project, run below command to create new migration
````
dotnet ef migrations add InitialMigration --startup-project ../MyApp.WebApi/MyApp.WebApi.csproj --output-dir Data/Migrations
````