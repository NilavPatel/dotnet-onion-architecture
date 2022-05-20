# Onion Architecture / Clean Architecture

<p align="center">
<img src="https://raw.githubusercontent.com/NilavPatel/dotnet-onion-architecture/main/docs/ddd-banner.png">
</p>

- Onion architecture can solve problem of separation of concern and tightly coupled components from N-layered architecture.
- All layers are depended on inner layer.
- The core of the application is the domain layer.
- Provide more testability than N-layered architecture.

<p align="center">
<img src="https://raw.githubusercontent.com/NilavPatel/dotnet-onion-architecture/main/docs/dotnet-onion-architecture.png">
</p>

## Layers

### Domain Layer:

This layer is not depend on any other layer. This layer contains entities, enums, specifications etc. related to the domain.
Also define Interfaces for repositories and third party services in this layer.

### Application Layer:

This layer contains business logic, services, service interfaces, request and response models. This layer is depends on domain layer only.

### Infrastructure Layer:

This layer contains database related logic (Repositories and DbContext), and third party library implementation (like a logger and email service). This implementation is based on domain layer.

### Presentation Layer:

This layer contains Webapi or UI.

## Domain model

Domain model are of 2 types

1. Domain entity (data only)
	- This model contains only fields

2. Domain model (data + behaviour)
	- This model has fields and behaviours. Fields can be modify only within behaviours.
	- Follow Aggregate pattern with Aggregate root, Value object, Entity, Bounded context, Ubiqutous language

## Validations in Domain driven design:

There are 2 types of validations in DDD:
1. Model Field validations
	- Properties having valid length
    - required field validations
    - regex

    Model validations can be validated in Application layer or Domain layer.

    - Use DataAnnotation (https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-6.0)
    - Use Guard pattern (https://github.com/NilavPatel/Guard-Pattern)
    - Use fluent validations pattern (https://docs.fluentvalidation.net/en/latest/aspnet.html)

2. Business validations
	- Balance should be more than Withdraw amount
	- User should be active 
	- User name should not be exist

    Business validations can be validated in Applciation layer or Domain layer.

    Business validations have two types:

    1. Validations in same domain model
        - Balance should be more than Withdraw amount
        - User should be active 
    2. Validations against other domain models
        - User name should not be exist

For Aggregate pattern add both types of validations inside domain layer.

Problem occurs when validating domain model against other domain models.

In this case use Func<> methods to pass validations to domain model from Application layer.

And run this Func<> from domain models.

Otherwise need to validate this type of validations in Application layer.

**For more details <a target="_blank" href="https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice">read</a>**

### Technologies Used:

- .Net 6
- Entity Framework
- NLog
- Swagger
- Xunit
- Moq
