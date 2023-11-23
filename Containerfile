# .NET Core SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Sets the working directory
WORKDIR /app

# Copy Projects
#COPY *.sln .
COPY Src/DDD.Application/DDD.Application.csproj ./Src/DDD.Application/
COPY Src/DDD.Domain/DDD.Domain.csproj ./Src/DDD.Domain/
COPY Src/DDD.Domain.Core/DDD.Domain.Core.csproj ./Src/DDD.Domain.Core/
COPY Src/DDD.Infra.CrossCutting.Bus/DDD.Infra.CrossCutting.Bus.csproj ./Src/DDD.Infra.CrossCutting.Bus/
COPY Src/DDD.Infra.CrossCutting.Identity/DDD.Infra.CrossCutting.Identity.csproj ./Src/DDD.Infra.CrossCutting.Identity/
COPY Src/DDD.Infra.CrossCutting.IoC/DDD.Infra.CrossCutting.IoC.csproj ./Src/DDD.Infra.CrossCutting.IoC/
COPY Src/DDD.Infra.Data/DDD.Infra.Data.csproj ./Src/DDD.Infra.Data/
COPY Src/DDD.Services.Api/DDD.Services.Api.csproj ./Src/DDD.Services.Api/

# .NET Core Restore
RUN dotnet restore ./Src/DDD.Services.Api/DDD.Services.Api.csproj

# Copy All Files
COPY Src ./Src

# .NET Core Build and Publish
RUN dotnet publish ./Src/DDD.Services.Api/DDD.Services.Api.csproj -c Release -o /publish

# ASP.NET Core Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /publish ./

# Expose ports
EXPOSE 80
EXPOSE 443

# Setup your variables before running.
ARG MyEnv
ENV ASPNETCORE_ENVIRONMENT $MyEnv

ENTRYPOINT ["dotnet", "DDD.Services.Api.dll"]
