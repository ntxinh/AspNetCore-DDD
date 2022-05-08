# .NET Core SDK
FROM mcr.microsoft.com/dotnet/core/sdk:6.0-alpine AS build

# Sets the working directory
WORKDIR /app

# Copy Projects
#COPY *.sln .
COPY Src/DDD.Application/DDD.Application.csproj ./DDD.Application/
COPY Src/DDD.Domain/DDD.Domain.csproj ./DDD.Domain/
COPY Src/DDD.Domain.Core/DDD.Domain.Core.csproj ./DDD.Domain.Core/
COPY Src/DDD.Infra.CrossCutting.Bus/DDD.Infra.CrossCutting.Bus.csproj ./DDD.Infra.CrossCutting.Bus/
COPY Src/DDD.Infra.CrossCutting.Identity/DDD.Infra.CrossCutting.Identity.csproj ./DDD.Infra.CrossCutting.Identity/
COPY Src/DDD.Infra.CrossCutting.IoC/DDD.Infra.CrossCutting.IoC.csproj ./DDD.Infra.CrossCutting.IoC/
COPY Src/DDD.Infra.Data/DDD.Infra.Data.csproj ./DDD.Infra.Data/
COPY Src/DDD.Services.Api/DDD.Services.Api.csproj ./DDD.Services.Api/

# .NET Core Restore
RUN dotnet restore ./DDD.Services.Api/DDD.Services.Api.csproj

# Copy All Files
COPY Src ./

# .NET Core Build and Publish
RUN dotnet publish ./DDD.Services.Api/DDD.Services.Api.csproj -c Release -o /publish

# ASP.NET Core Runtime
FROM mcr.microsoft.com/dotnet/core/aspnet:6.0-alpine AS runtime
WORKDIR /app
COPY --from=build /publish ./
#EXPOSE 80
#EXPOSE 443
ENTRYPOINT ["dotnet", "DDD.Services.Api.dll"]
