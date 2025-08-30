# Restaurant Table Booking API

A RESTful API built with ASP.NET Core following Clean Architecture principles.  
This API allows you to manage restaurant table bookings with full CRUD operations.

## Project Structure

BookTableReservation
│
├─ Domain
│   └─ Entities
├─ Application
│   └─ Interfaces
├─ Infrastructure
│   ├─ Migrations 
│   └─ Repositories
│       └─ _GenericRepository
└─ BookTableReservation.csproj

## Features

- CRUD operations for Tables and Bookings
- Entity Framework Core integration
- DbProjectContext: manages database access
- Code First Approach
- Migrations: track and apply schema changes
- Clean Architecture (Separation of Concerns)
- Validation with Data Annotations & Fluent API
- Swagger UI for API testing
- JWT Authentication
- Generic Repository for reusable data access
- SQL Server support

## Installation

```bash
# Clone the repository
git clone https://github.com/elnazQofrani/API.NetCore_CleanArchitect_TableReservation.git

# Navigate to project folder
cd API.NetCore_CleanArchitect_TableReservation

# Restore dependencies
dotnet restore

# Run the Web API
dotnet run --project BookTableReservation/BookTableReservation.csproj
