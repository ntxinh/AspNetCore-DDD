# Diagram
![](/docs/diagram.jpg)

![](/docs/architecture.png)

# Techical Stack
- ASP.NET Core 3.1 (with .NET Core 3.1)
- ASP.NET WebApi Core
- ASP.NET Identity Core
- Entity Framework Core 3.1
- .NET Core Native DI
- AutoMapper
- FluentValidator
- MediatR
- Swagger UI
- MSSQL

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

# How to run
```sh
dotnet run --project src/DDD.Services.Api/DDD.Services.Api.csproj --launch-profile Dev
```

# Swagger
- http://localhost:5000/swagger/index.html

# References
- https://github.com/EduardoPires/EquinoxProject
- https://docs.microsoft.com/en-us/aspnet/core/security/authorization/roles?view=aspnetcore-3.1
- https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims?view=aspnetcore-3.1
- https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-3.1
- https://docs.microsoft.com/en-us/aspnet/core/security/authorization/limitingidentitybyscheme?view=aspnetcore-3.1
- https://www.red-gate.com/simple-talk/dotnet/c-programming/policy-based-authorization-in-asp-net-core-a-deep-dive/
- https://docs.microsoft.com/en-us/archive/msdn-magazine/2017/october/cutting-edge-policy-based-authorization-in-asp-net-core
- https://dev.azure.com/Techhowdy/_git/NG_Core_AuthRTDB
- https://github.com/Elfocrash/Youtube.AspNetCoreTutorial

# TODO
- [x] Use multiple environments
- [x] Transaction (Unit of Work)
- [x] Validation (FluentValidation)
- [x] Response wrapper
- [x] JWT
- [x] Mapping (AutoMapper)
- [x] API Specification, API Definition (Swagger)
- [x] ORM {Entity Framework Core}
- [x] Middleware
- [x] CORS
- [ ] Migration, Scaffold
- [ ] Data Seeding
- [ ] Logging
- [ ] Pagination
- [ ] Sorting
- [ ] OAuth2
- [ ] Error Handling, Global Exception
- [ ] SignalR
