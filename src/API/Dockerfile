# Use official .NET 8 SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ../*.sln .
COPY API/*.csproj ./API/
COPY Application/*.csproj ./Application/
COPY Domain/*.csproj ./Domain/
COPY Infrastructure/*.csproj ./Infrastructure/

RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /app/API
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/API/out ./

# Expose port
EXPOSE 5000
EXPOSE 5001

# Run the application
ENTRYPOINT ["dotnet", "API.dll"]
