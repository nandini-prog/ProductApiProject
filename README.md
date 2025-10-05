Product API Solution
Overview

This repository contains a RESTful backend API for managing products, built using .NET 8 and following industry best practices.
The API supports CRUD operations for products, with authentication, validation, logging, and testing.

Table of Contents

Architecture

Tech Stack

Database Structure

API Endpoints

Getting Started

Docker Setup

Testing

Performance & Security

Documentation

Architecture

The solution follows a layered architecture:

Solution/
├── src/
│   ├── API/                  # ASP.NET Core Web API
│   ├── Application/          # Business logic
│   ├── Domain/               # Domain models
│   └── Infrastructure/       # Data access, identity, logging
├── tests/
│   ├── API.Tests/            # Integration tests
│   ├── Application.Tests/    # Unit tests
│   └── Infrastructure.Tests/ # Unit tests
└── docker-compose.yml        # Docker setup

Tech Stack

Framework: .NET 8 (C#)

API Framework: ASP.NET Core Web API

Database: SQL Server + EF Core

Authentication: JWT with refresh tokens

Testing: xUnit, Moq, WebApplicationFactory

Documentation: Swagger/OpenAPI + JSDoc comments

Containerization: Docker + Docker Compose

Logging: Structured logging

Database Structure
CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
	[ProductName] NVARCHAR(255) NOT NULL,
	[CreatedBy] NVARCHAR(100) NOT NULL,
	[CreatedOn] DATETIME NOT NULL,
	[ModifiedBy] NVARCHAR(100) NULL,
	[ModifiedOn] DATETIME NULL
)

CREATE TABLE [dbo].[Item]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY (1,1),
    [ProductId] INT NOT NULL FOREIGN KEY REFERENCES Product(Id),
    [Quantity] INT NOT NULL
)

API Endpoints

Example endpoints for Product resource:

Method	Endpoint	Description
GET	/api/v1/products	Get all products
GET	/api/v1/products/{id}	Get product by ID
POST	/api/v1/products	Create a new product
PUT	/api/v1/products/{id}	Update an existing product
DELETE	/api/v1/products/{id}	Delete a product

All endpoints use JSON for request/response and standard HTTP status codes.

Getting Started
Prerequisites

.NET 8 SDK

Docker

SQL Server
 (optional if using Docker)

Running Locally (without Docker)

Update the connection string in appsettings.json to your SQL Server.

Apply migrations:

dotnet ef database update --project Infrastructure --startup-project API


Run the API:

cd src/API
dotnet run


Access Swagger: https://localhost:5001/swagger

Docker Setup
1. Build and Run
docker-compose up --build

2. Features

SQL Server container with persistent volume

API container with automatic EF Core migrations

Health check ensures SQL Server is ready before API starts

Ports exposed:

API: 5000 (HTTP) / 5001 (HTTPS)

SQL Server: 1433

Testing

Unit tests: Application & Infrastructure layers using xUnit + Moq

Integration tests: API layer using WebApplicationFactory

Run all tests:

dotnet test

Performance & Security

Async/await for all database operations

Pagination for collection endpoints

Database indexing & AsNoTracking for reads

JWT authentication with refresh tokens

Role-based authorization

Input validation via FluentValidation

HTTPS, CORS policy, security headers

Documentation

OpenAPI/Swagger documentation for all endpoints: https://localhost:5001/swagger

JSDoc comments in code for detailed method-level documentation

Authentication Flow:

JWT access token with short expiry

Refresh token rotation

Role-based authorization checks

Environment Setup Instructions:

Install .NET 8 SDK

Configure SQL Server connection string

Apply EF Core migrations

Deployment Procedures:

Docker build and run using docker-compose up --build

Automatic database migrations on API container startup

Ports exposed for API and SQL Server
