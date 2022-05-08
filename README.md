# Diagram

## Overview
![](/Docs/overview.png)

## Architecture
![](/Docs/architecture.png)

## Dependencies
![](/Docs/dependencies.jpg)

## Project dependencies
![](/Docs/project-dependencies.jpg)

## Code flow
![](/Docs/flowchart.png)
![](/Docs/code-flow.jpg)

## Repository & Unit Of Work
![](/Docs/custom-repo-versus-db-context.png)

# Techical Stack
- ASP.NET Core 6.0 (with .NET 6.0)
- ASP.NET WebApi Core
- ASP.NET Identity Core
- Entity Framework Core
- .NET Core Native DI
- AutoMapper
- FluentValidator
- MediatR
- Swagger UI
- MSSQL
- xUnit
- Moq
- Fluent Assertions
- Polly
- Refit
- DbUp

# Design Patterns
- Domain Driven Design
- Domain Events
- Domain Notification
- CQRS
- Event Sourcing
- Unit Of Work
- Repository & Generic Repository
- Inversion of Control / Dependency injection
- ORM
- Mediator
- Specification Pattern
- Options Pattern

# How to run
- Create file `C:\Users\[UserName]\AppData\Roaming\Microsoft\UserSecrets\51c0770a-8c88-4362-b3b5-a8936796ecef\secrets.json`

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=DDD;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

- For Visual Studio: `Select profile > Run (F5)`
- For VSCode: `Select configuration > Run (F5)`
- For Terminal:

```ps
dotnet build Src/DDD.Services.Api/DDD.Services.Api.csproj
dotnet run --project Src/DDD.Services.Api/DDD.Services.Api.csproj --launch-profile Dev
```

# Testing
- Terminal: `dotnet test`

# Docker

```sh
docker build -t aspnetcore-docker-image .
docker run -it --rm -p 3000:80 --name aspnetcore-docker-container aspnetcore-docker-image
docker run -d -p 3000:80 --name aspnetcore-docker-container aspnetcore-docker-image
```

- http://localhost:3000/

# Swagger (Dev env only)
- http://localhost:5000/swagger

# Health check (Staging & Prod env only)
- http://localhost:5000/hc-ui
- http://localhost:5000/hc-json

# TODO
- [x] Use multiple environments
- [x] Transaction (Unit of Work)
- [x] Validation (FluentValidation)
- [x] Response wrapper
- [x] Async/Await
- [x] REST
- [x] JWT
- [x] Mapping (AutoMapper)
- [x] API Specification, API Definition (Swagger)
- [x] ORM {Entity Framework Core}
- [x] Middleware
- [x] CORS
- [x] Pagination
- [x] Sorting
- [x] Error Handling, Global Exception
- [x] HealthCheck
- [x] Mail
- [x] Http
- [x] Database Auditing: CreatedAt/UpdatedAt CreatedBy/UpdatedBy
- [x] Soft Delete
- [x] Common: Constants, Helpers
- [x] Docker
- [x] EF: Shadow Properties
- [x] Events
- [x] Unit Testing
- [x] Integration Testing
- [x] Scoped over Transient
- [x] Use `abstract` keyword to appropriate class
- [x] Use `IQueryable`, `IEnumerable`, `IList` interfaces
- [x] Use NetStandard 2.1 for Class Library
- [x] Hashing
- [x] AnalysisLevel: Automatically find latent bugs
- [x] Migration (DbUp)
- [x] User Secrets
- [ ] Scaffold
- [ ] Data Seeding
- [ ] Logging
- [ ] OAuth2, OIDC (OpenId Connect)
- [ ] SignalR
- [ ] Search
- [ ] Kafka, RabbitMQ
- [ ] Microservices, API Gateway
- [ ] Multi-tenancy
- [ ] StyleCop
- [ ] API Versioning
- [ ] API Versioning with Swagger
- [ ] Primary Key to Integer
- [ ] File storage: Upload/Download
- [ ] Kubernetes
- [ ] Globalization & Localization
- [ ] Caching
- [ ] Kestrel
- [ ] Secret Manager
- [ ] Task scheduling & Queues
- [ ] Session & Cookie
- [ ] Notifications
- [ ] Encryption
- [ ] EF: No-tracking queries
- [ ] Dapper, Dapper Contrib (Optional)
- [ ] BulkInsert, BulkUpdate, Async method for IRepository

# References
- https://github.com/EduardoPires/EquinoxProject
- https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles
- https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims
- https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies
- https://docs.microsoft.com/en-us/aspnet/core/security/authorization/limitingidentitybyscheme
- https://www.red-gate.com/simple-talk/dotnet/c-programming/policy-based-authorization-in-asp-net-core-a-deep-dive/
- https://docs.microsoft.com/en-us/archive/msdn-magazine/2017/october/cutting-edge-policy-based-authorization-in-asp-net-core
- https://dev.azure.com/Techhowdy/_git/NG_Core_AuthRTDB
- https://github.com/Elfocrash/Youtube.AspNetCoreTutorial
- https://www.meziantou.net/entity-framework-core-soft-delete-using-query-filters.htm
- https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments
- https://michael-mckenna.com/multi-tenant-asp-dot-net-core-application-tenant-resolution
- https://stackify.com/writing-multitenant-asp-net-core-applications/
- https://github.com/DanielRBowen/SimpleMultiTenant
- https://deviq.com/specification-pattern/
- https://devblogs.microsoft.com/dotnet/automatically-find-latent-bugs-in-your-code-with-net-5/
- https://xunit.github.io/docs/why-no-netstandard
