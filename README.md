# Dotnet Onion Architecture / Clean Architecture

## Domain Layer.

This layer is not depended on any layer. This layer contains entities.

## Application Layer.

This layer contains business logic and other interfaces (For repository and third party libraries).

## Infrastructure Layer.

This layer contains database related logic (Repositories and DbContext), and third party libraries implementation (like logger and email service).

## Presentation Layer

This layer contains Webapi or UI.

<img src="https://raw.githubusercontent.com/NilavPatel/dotnet-onion-architecture/main/docs/dotnet-onion-architecture.png" style="padding:10px">
