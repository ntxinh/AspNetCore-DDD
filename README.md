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
- ASP.NET Core 8.0 (with .NET 8.0)
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

# Pre-Configuration

- Config User Secret:
  + Find `<user_secrets_id>` at `DDD.Services.Api.csproj` > `UserSecretsId` (Free to change to any GUID/UUID)
  + Windows: `C:\Users\[UserName]\AppData\Roaming\Microsoft\UserSecrets\<user_secrets_id>\secrets.json`
  + Linux / macOS: `~/.microsoft/usersecrets/<user_secrets_id>/secrets.json`


- `secrets.json` for Windows:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=DDD;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

- LocalDB is a packaging mechanism for SQL Server Express Edition, and is only available for Windows, use [Docker Images](https://hub.docker.com/_/microsoft-mssql-server) for Linux / macOS

- `secrets.json` for Linux / macOS:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=<ip_address>,1433;Initial Catalog=aspnetcore-ddd;User ID=SA;pwd=<YourNewStrong@Passw0rd>;Integrated Security=False;ConnectRetryCount=0;MultipleActiveResultSets=True"
  }
}
```

# How to run

- For Visual Studio: `Select profile > Run (F5)`
- For VSCode: `Select configuration > Run (F5)`
- For Terminal:

```bash
dotnet build Src/DDD.Services.Api/DDD.Services.Api.csproj
dotnet run --project Src/DDD.Services.Api/DDD.Services.Api.csproj --launch-profile Dev
dotnet watch --project Src/DDD.Services.Api/DDD.Services.Api.csproj run
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

```bash
docker compose up -d
docker compose ps
docker compose stop
```

- http://localhost:80/

# Podman

```bash
podman build -t aspnetcore-docker-image .
podman run -it --rm -p 3000:80 --name aspnetcore-docker-container aspnetcore-docker-image
podman run -d -p 3000:80 --name aspnetcore-docker-container aspnetcore-docker-image
```

- http://localhost:3000/

```bash
podman-compose up -d
podman-compose ps
podman-compose stop
```

- http://localhost:80/

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
- [x] Docker, Docker Compose, Github Container Registry
- [x] EF: Shadow Properties
- [x] Events
- [x] Unit Testing
- [x] Integration Testing
- [x] Scoped over Transient
- [x] Use `abstract` keyword to appropriate class
- [x] Use `IQueryable`, `IEnumerable`, `IList` interfaces
- [x] Use NetStandard 2.1 for Class Library
- [x] Hashing
- [x] ~~AnalysisLevel: Automatically find latent bugs~~ (It was enabled by default for .NET 5 or above)
- [x] Migration (DbUp)
- [x] User Secrets
- [x] API Versioning
- [x] API Versioning with Swagger
- [x] Kubernetes
- [x] AKS
- [x] Hot reload
- [x] SignalR
- [x] Notifications
- [x] Webhook
- [x] Task scheduling & Queues: Quartz
- [x] Quartz: Fire-and-forget
- [x] NPOI
- [x] REST Client
- [x] [StyleCopAnalyzers](https://github.com/DotNetAnalyzers/StyleCopAnalyzers) (Use [default rules set](https://github.com/DotNetAnalyzers/StyleCopAnalyzers/blob/master/StyleCop.Analyzers/StyleCop.Analyzers.CodeFixes/rulesets/StyleCopAnalyzersDefault.ruleset) and disable 9 rules: SA0001, SA1200, SA1201, SA1309, SA1400, SA1512, SA1600, SA1601, SA1633)
- [x] [RoslynAnalyzers](https://github.com/dotnet/roslyn-analyzers) (It was enabled by default for .NET 5 or above)
- [ ] [OmniSharp Roslyn](https://github.com/OmniSharp/omnisharp-roslyn)
- [ ] [sonar-dotnet](https://github.com/SonarSource/sonar-dotnet)
- [ ] Autofac or Scrutor
- [ ] Bogus
- [ ] Scaffold
- [ ] Data Seeding
- [ ] Logging
- [ ] OAuth2, OIDC (OpenId Connect)
- [ ] Search
- [ ] Kafka, RabbitMQ
- [ ] Microservices, API Gateway (Ocelot, yarp)
- [ ] Multi-tenancy
- [ ] Primary Key to Integer
- [ ] File storage: Upload/Download
- [ ] Globalization & Localization
- [ ] Caching
- [ ] Kestrel
- [ ] Secret Manager
- [ ] Session & Cookie
- [ ] Encryption
- [ ] EF: No-tracking queries
- [ ] Dapper, Dapper Contrib (Optional)
- [ ] RepoDB
- [ ] BulkInsert, BulkUpdate, Async method for IRepository
- [ ] [MassTransit](https://github.com/MassTransit/MassTransit), [NServiceBus](https://github.com/Particular/NServiceBus)

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
