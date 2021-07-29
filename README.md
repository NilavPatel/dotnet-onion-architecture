# Onion Architecture / Clean Architecture

- Onion architecture can solve problem of separation of concern and tightly coupled components from N-layered architecture.
- All layers are depended on inner layer.
- The core of the application is the domain layer.
- Provide more testability than N-layered architecture.

<img src="https://raw.githubusercontent.com/NilavPatel/dotnet-onion-architecture/main/docs/dotnet-onion-architecture.png" style="padding:10px">

### Domain Layer:

This layer is not dependent on any layer. This layer contains entities, enums, specifications etc. related to the domain.  

### Application Layer:

This layer contains business logic, services, service interfaces, repository interfaces, other third party library interfaces, request and response models. This layer is dependent on domain layer only.  

### Infrastructure Layer:

This layer contains database related logic (Repositories and DbContext), and third party library implementation (like a logger and email service). This implementation is based on application layer.

### Presentation Layer:

This layer contains Webapi or UI.  

### Technologies:

- Asp.Net Core
- Entity Framework Core
- NLog
- Swagger
- Xunit
- Moq
