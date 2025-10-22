# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Mikroservice.Order.Api/Mikroservice.Order.Api.csproj", "Mikroservice.Order.Api/"]
COPY ["Microservice.Order.Application/Microservice.Order.Application.csproj", "Microservice.Order.Application/"]
COPY ["Microservice.Bus/Microservice.Bus.csproj", "Microservice.Bus/"]
COPY ["Microservice.Order.Domain/Microservice.Order.Domain.csproj", "Microservice.Order.Domain/"]
COPY ["Microservice.Shared/Microservice.Shared.csproj", "Microservice.Shared/"]
COPY ["Microservice.Order.Persistence/Microservice.Order.Persistence.csproj", "Microservice.Order.Persistence/"]
RUN dotnet restore "./Mikroservice.Order.Api/Mikroservice.Order.Api.csproj"
COPY . .
WORKDIR "/src/Mikroservice.Order.Api"
RUN dotnet build "./Mikroservice.Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Mikroservice.Order.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mikroservice.Order.Api.dll"]