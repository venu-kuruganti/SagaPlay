# Base runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files and restore dependencies
COPY ["ApiGateway/ApiGateway.csproj", "ApiGateway/"]
COPY ["SagaPlay.Shared.Contracts/SagaPlay.Shared.Contracts.csproj", "SagaPlay.Shared.Contracts/"]
RUN dotnet restore "ApiGateway/ApiGateway.csproj"

# Copy the rest of the code
COPY . .

# Build the app
WORKDIR "/src/ApiGateway"
RUN dotnet build "ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ApiGateway.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy the Ocelot configs
COPY ["ApiGateway/ocelot.json", "ApiGateway/ocelot.Production.json", "./"]

# Default environment (can be overridden)
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "ApiGateway.dll"]
