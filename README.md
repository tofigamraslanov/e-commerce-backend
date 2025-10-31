# E-Commerce Backend API

A comprehensive, enterprise-grade e-commerce backend application built with .NET 6, following Clean Architecture principles and implementing CQRS pattern with MediatR.

## 📋 Table of Contents

- [Overview](#overview)
- [Architecture](#architecture)
- [Technologies](#technologies)
- [Features](#features)
- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Configuration](#configuration)
  - [Database Setup](#database-setup)
  - [Running the Application](#running-the-application)
- [API Documentation](#api-documentation)
- [Authentication & Authorization](#authentication--authorization)
- [File Storage](#file-storage)
- [Logging](#logging)
- [Real-time Communication](#real-time-communication)
- [Contributing](#contributing)
- [License](#license)

## 🎯 Overview

This project is a full-featured e-commerce backend API that provides comprehensive functionality for managing products, orders, customers, and file uploads. It implements modern software development practices including Clean Architecture, CQRS pattern, Domain-Driven Design (DDD), and includes real-time capabilities through SignalR.

## 🏗️ Architecture

The application follows **Clean Architecture** (Onion Architecture) with clear separation of concerns:

```
┌─────────────────────────────────────────┐
│         Presentation Layer              │
│         (ECommerceBackend.API)          │
└─────────────────────────────────────────┘
                    │
┌─────────────────────────────────────────┐
│       Infrastructure Layer              │
│  ┌──────────────────────────────────┐   │
│  │  ECommerceBackend.Infrastructure │   │
│  │  (Services, Storage, Filters)    │   │
│  └──────────────────────────────────┘   │
│  ┌──────────────────────────────────┐   │
│  │  ECommerceBackend.Persistence    │   │
│  │  (DbContext, Repositories)       │   │
│  └──────────────────────────────────┘   │
│  ┌──────────────────────────────────┐   │
│  │  ECommerceBackend.SignalR        │   │
│  │  (Hubs, Real-time Services)      │   │
│  └──────────────────────────────────┘   │
└─────────────────────────────────────────┘
                    │
┌─────────────────────────────────────────┐
│           Core Layer                    │
│  ┌──────────────────────────────────┐   │
│  │  ECommerceBackend.Application    │   │
│  │  (Features, DTOs, Validators)    │   │
│  └──────────────────────────────────┘   │
│  ┌──────────────────────────────────┐   │
│  │  ECommerceBackend.Domain         │   │
│  │  (Entities, Interfaces)          │   │
│  └──────────────────────────────────┘   │
└─────────────────────────────────────────┘
```

### Architectural Patterns

- **Clean Architecture**: Ensures dependency inversion and separation of concerns
- **CQRS (Command Query Responsibility Segregation)**: Separates read and write operations using MediatR
- **Repository Pattern**: Abstracts data access logic
- **Mediator Pattern**: Decouples request handling from business logic
- **Options Pattern**: Manages configuration settings

## 🚀 Technologies

### Core Frameworks
- **.NET 6** - Cross-platform framework
- **ASP.NET Core 6** - Web API framework
- **Entity Framework Core 6** - ORM for database operations
- **ASP.NET Core Identity** - User authentication and authorization

### Libraries & Tools
- **MediatR** - Mediator pattern implementation for CQRS
- **FluentValidation** - Input validation
- **AutoMapper** - Object-to-object mapping
- **SignalR** - Real-time web functionality
- **Serilog** - Structured logging
- **JWT Bearer Authentication** - Token-based authentication
- **Swagger/OpenAPI** - API documentation

### Storage & Database
- **PostgreSQL** - Primary database
- **Azure Blob Storage** - Cloud file storage
- **Entity Framework Core Migrations** - Database version control

### Logging & Monitoring
- **Serilog** - Structured logging with multiple sinks:
  - Console
  - File
  - PostgreSQL
  - Seq (centralized logging)

## ✨ Features

### Product Management
- Create, read, update, and delete products
- Product image management (upload, delete, set showcase image)
- Pagination support for product listings
- Product image gallery management
- Stock management

### User Management
- User registration and authentication
- JWT-based authentication
- Refresh token support
- Social login integration:
  - Google OAuth
  - Facebook Login
- Role-based authorization

### Order & Customer Management
- Order processing
- Customer information management
- Invoice file handling

### File Management
- Multiple storage providers (Local, Azure Blob Storage)
- Product image uploads
- Invoice file management
- Configurable storage backend

### Real-time Features
- SignalR hubs for real-time communication
- Product update notifications
- Real-time data synchronization

### Security
- JWT Bearer token authentication
- Role-based access control (Admin authentication scheme)
- Secure password hashing via ASP.NET Core Identity
- CORS configuration for controlled access

### Validation & Error Handling
- FluentValidation for request validation
- Custom exception handling
- Automatic model state validation
- Structured error responses

### Logging & Monitoring
- Comprehensive logging with Serilog
- Multiple log sinks (Console, File, PostgreSQL, Seq)
- HTTP request/response logging
- User context enrichment

##  Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [PostgreSQL 12+](https://www.postgresql.org/download/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Azure Storage Account](https://azure.microsoft.com/en-us/services/storage/) (optional, for cloud storage)
- [Seq](https://datalust.co/seq) (optional, for centralized logging)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/tofigamraslanov/e-commerce-backend.git
   cd e-commerce-backend
   ```

2. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

3. **Build the solution**
   ```bash
   dotnet build
   ```

### Configuration

1. **Update `appsettings.json`** in the `Presentation/ECommerceBackend.API` folder:

```json
{
  "ConnectionStrings": {
    "PostgreSQL": "User ID=your_user;Password=your_password;Host=localhost;Port=5432;Database=ECommerceAppDb;"
  },
  "Token": {
    "Audience": "www.myclient.com",
    "Issuer": "www.myapi.com",
    "SecurityKey": "your-secret-key-minimum-32-characters"
  },
  "Storage": {
    "Azure": "your-azure-storage-connection-string"
  },
  "BaseStorageUrl": "https://yourstorageaccount.blob.core.windows.net",
  "GoogleLogin": {
    "ClientId": "your-google-client-id"
  },
  "FacebookLogin": {
    "ClientId": "your-facebook-app-id",
    "ClientSecret": "your-facebook-app-secret"
  },
  "Seq": {
    "ServerURL": "http://localhost:5341/"
  }
}
```

2. **Configure Storage Provider** in `Program.cs`:
   
   For Azure Storage:
   ```csharp
   builder.Services.AddStorage<AzureStorage>();
   ```
   
   For Local Storage:
   ```csharp
   builder.Services.AddStorage<LocalStorage>();
   ```

### Database Setup

1. **Create PostgreSQL Database**
   ```sql
   CREATE DATABASE ECommerceAppDb;
   ```

2. **Apply Migrations**
   ```bash
   cd Presentation/ECommerceBackend.API
   dotnet ef database update --project ../../Infrastructure/ECommerceBackend.Persistence
   ```

3. **Verify Database Creation**
   - Connect to PostgreSQL and verify tables are created
   - Check for `Products`, `Orders`, `Customers`, `AspNetUsers`, etc.

### Running the Application

1. **Run the API**
   ```bash
   cd Presentation/ECommerceBackend.API
   dotnet run
   ```

2. **Access Swagger UI**
   - Open browser and navigate to: `https://localhost:5001/swagger`
   - Or: `http://localhost:5000/swagger`

3. **Test the API**
   - Use Swagger UI to explore and test endpoints
   - Use tools like Postman or curl for API testing

## 📚 API Documentation

### Base URL
```
Development: https://localhost:5001/api
Production: https://yourapi.com/api
```

### Authentication Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| POST | `/api/auth/login` | User login with credentials | No |
| POST | `/api/auth/refreshtokenlogin` | Refresh access token | No |
| POST | `/api/auth/google-login` | Login with Google | No |
| POST | `/api/auth/facebook-login` | Login with Facebook | No |

### Product Endpoints

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|---------------|
| GET | `/api/products` | Get all products (paginated) | No |
| GET | `/api/products/{id}` | Get product by ID | No |
| POST | `/api/products` | Create new product | Yes (Admin) |
| PUT | `/api/products` | Update product | Yes (Admin) |
| DELETE | `/api/products/{id}` | Delete product | Yes (Admin) |
| POST | `/api/products/upload?id={id}` | Upload product images | Yes (Admin) |
| GET | `/api/products/getproductimages/{id}` | Get product images | Yes (Admin) |
| DELETE | `/api/products/deleteproductimage/{id}?imageId={imageId}` | Delete product image | Yes (Admin) |
| GET | `/api/products/changeshowcaseimage?imageId={imageId}&productId={productId}` | Set showcase image | Yes (Admin) |

### Request Examples

**Login Request**
```json
POST /api/auth/login
{
  "usernameOrEmail": "admin@example.com",
  "password": "SecurePassword123!"
}
```

**Response**
```json
{
  "token": {
    "accessToken": "eyJhbGciOiJIUzI1NiIs...",
    "expiration": "2024-10-31T12:00:00Z",
    "refreshToken": "refresh-token-string"
  }
}
```

**Create Product Request**
```json
POST /api/products
Authorization: Bearer {token}

{
  "name": "Laptop XYZ",
  "stock": 50,
  "price": 999.99
}
```

**Get Products with Pagination**
```
GET /api/products?page=1&size=10
```

## 🔐 Authentication & Authorization

### JWT Configuration

The API uses JWT Bearer token authentication with the following configuration:

- **Token Lifetime**: Configurable in `appsettings.json`
- **Refresh Token**: Supported for extending sessions
- **Claims**: User identity, roles, and custom claims

### Authentication Flow

1. User logs in with credentials or social login
2. Server validates credentials
3. Server generates JWT access token and refresh token
4. Client stores tokens securely
5. Client includes access token in `Authorization` header for protected endpoints
6. When access token expires, use refresh token to get new access token

### Authorization Schemes

- **Admin**: Required for product management, file operations
- **User**: For user-specific operations

### Using Authentication

Include the JWT token in request headers:
```
Authorization: Bearer {your-jwt-token}
```

### Social Login Setup

**Google Login:**
1. Create OAuth 2.0 credentials in [Google Cloud Console](https://console.cloud.google.com/)
2. Add Client ID to `appsettings.json`
3. Configure authorized redirect URIs

**Facebook Login:**
1. Create app in [Facebook Developers](https://developers.facebook.com/)
2. Add App ID and Secret to `appsettings.json`
3. Configure OAuth redirect URLs

## 💾 File Storage

The application supports multiple storage providers through abstraction:

### Azure Blob Storage (Default)
- Cloud-based storage
- Scalable and reliable
- Configured via connection string in `appsettings.json`

### Local Storage
- File system storage
- Good for development
- Files stored in `wwwroot` folder

### Configuration

Switch storage providers in `Program.cs`:
```csharp
// Azure Storage
builder.Services.AddStorage<AzureStorage>();

// OR Local Storage
builder.Services.AddStorage<LocalStorage>();
```

### File Operations
- Product image uploads (multiple files)
- Image deletion
- Showcase image selection
- Automatic file naming and organization

## 📊 Logging

### Serilog Configuration

The application uses Serilog for structured logging with multiple sinks:

1. **Console Sink**: Real-time console output
2. **File Sink**: Log files in `Logs/Log.txt`
3. **PostgreSQL Sink**: Database logging for persistence
4. **Seq Sink**: Centralized log server (optional)

### Log Levels
- Information: General application flow
- Warning: Unexpected but handled situations
- Error: Application errors and exceptions
- Debug: Detailed diagnostic information

### HTTP Logging
- Request/response logging enabled
- Custom header logging
- Request/response body logging (4KB limit)

### Accessing Logs

**File Logs:**
```
/Logs/Log.txt
```

**PostgreSQL Logs:**
```sql
SELECT * FROM "Logs" ORDER BY timestamp DESC;
```

**Seq Dashboard:**
```
http://localhost:5341
```

## 🔄 Real-time Communication

### SignalR Implementation

The application uses SignalR for real-time features:

- Product update notifications
- Real-time data synchronization
- Hub-based architecture

### Hub Services
- `IProductHubService`: Product-related real-time updates
- Extensible for additional hub services

### Client Connection

Connect to SignalR hubs from client applications:
```javascript
const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:5001/product-hub")
    .build();

await connection.start();
```

## 🛠️ Development

### Adding New Features

1. **Define Entity** in `Domain/Entities`
2. **Create Repository Interface** in `Application/Repositories`
3. **Implement Repository** in `Persistence/Repositories`
4. **Create CQRS Handlers** in `Application/Features`
5. **Add Controller Endpoint** in `API/Controllers`
6. **Add Validation** using FluentValidation
7. **Create Migration** and update database

### Running Migrations

**Add new migration:**
```bash
dotnet ef migrations add MigrationName --project Infrastructure/ECommerceBackend.Persistence --startup-project Presentation/ECommerceBackend.API
```

**Update database:**
```bash
dotnet ef database update --project Infrastructure/ECommerceBackend.Persistence --startup-project Presentation/ECommerceBackend.API
```

**Remove last migration:**
```bash
dotnet ef migrations remove --project Infrastructure/ECommerceBackend.Persistence --startup-project Presentation/ECommerceBackend.API
```

### Code Standards
- Follow SOLID principles
- Use async/await for I/O operations
- Implement proper exception handling
- Write unit tests for business logic
- Use dependency injection
- Follow naming conventions

## 🧪 Testing

### Project Structure for Tests (Recommended)
```
tests/
├── ECommerceBackend.UnitTests/
├── ECommerceBackend.IntegrationTests/
└── ECommerceBackend.ApiTests/
```

### Running Tests
```bash
dotnet test
```

## 📦 Deployment

### Building for Production

```bash
dotnet publish -c Release -o ./publish
```

### Docker Support (Future Enhancement)

Create `Dockerfile` for containerization:
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ECommerceBackend.API.dll"]
```

### Environment Variables

Set the following environment variables in production:
- `ASPNETCORE_ENVIRONMENT=Production`
- `ConnectionStrings__PostgreSQL`
- `Token__SecurityKey`
- `Storage__Azure`

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

### Contribution Guidelines
- Follow the existing code style
- Write meaningful commit messages
- Add tests for new features
- Update documentation as needed
- Ensure all tests pass before submitting PR

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👥 Authors

- **Tofig Amraslanov** - [GitHub](https://github.com/tofigamraslanov)

## 🙏 Acknowledgments

- Clean Architecture principles by Robert C. Martin
- CQRS pattern implementation with MediatR
- ASP.NET Core team for the excellent framework
- Community contributors and supporters

## 📞 Support

For support and questions:
- Create an issue in the GitHub repository
- Contact: tofigamraslanoc01@gmail.com

---

**Built with ❤️ using .NET 6 and Clean Architecture principles**
