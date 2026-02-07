# Reddit Management System

## 📋 Project Overview
A .NET-based automated system that scrapes top posts from Reddit subreddits, processes the data, and sends email notifications to specific users. The system uses background jobs for scheduled tasks and follows Clean Architecture principles.

## 🎯 Purpose
- Fetch top posts from multiple Reddit subreddits (r/microsaas, r/SideProject)
- Process and filter posts based on upvotes
- Automatically send email notifications with curated content
- Manage background jobs using Quartz.NET for scheduled execution

## 🏗️ Architecture

### Clean Architecture Layers

```
┌─────────────────────────────────────────────────────────┐
│                    Presentation Layer                    │
│              (Reddit-Management-System)                  │
│         Controllers, Middleware, Extensions              │
└─────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────┐
│                   Application Layer                      │
│         (Reddit-Management-System.Application)           │
│    Features, DTOs, MediatR, Interfaces, Utilities       │
└─────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────┐
│                     Service Layer                        │
│           (Reddit-Management-System.Service)             │
│        Business Logic, Validators, Background Jobs       │
└─────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────┐
│                   Repository Layer                       │
│            (Reddit-Management-System.Repo)               │
│              Data Access Implementation                  │
└─────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────┐
│                Infrastructure Layer                      │
│            (Reddit-Management-System.Data)               │
│         DbContext, Migrations, Configurations            │
└─────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────┐
│                     Domain Layer                         │
│           (Reddit-Management-System.Domain)              │
│              Entities, Interfaces, Constants             │
└─────────────────────────────────────────────────────────┘
```

## 🔑 Key Features

### 1. **CQRS Pattern with MediatR**
- Separation of Command and Query responsibilities
- `ICommand` / `ICommandHandler` for write operations
- `IQuery` / `IQueryHandler` for read operations

### 2. **Read/Write Separation**
- `IApplicationDbContext` for write operations
- `IReadDbContext` for read operations
- Optimized database access patterns

### 3. **Background Job Management**
- **Quartz.NET** for scheduled tasks
- `RedditEmailJob` runs daily at 9:00 AM
- Automated Reddit data scraping and email delivery

### 4. **Email Service**
- SMTP integration using MailKit
- Configurable email templates
- Automated notification system

## 📁 Project Structure

```
Reddit-Management-System/
│
├── Reddit-Management-System/              # Presentation Layer
│   ├── Controllers/
│   │   ├── Permission/
│   │   ├── Role/
│   │   ├── Test/
│   │   │   └── TestController.cs         # Reddit scraping endpoint
│   │   └── Users/
│   ├── DependencyExtensions/
│   │   ├── JWTAuthentication.cs
│   │   ├── RegisterAuthPolicy.cs
│   │   ├── RegisterBackgroundJobs.cs     # Quartz.NET configuration
│   │   ├── RegisterCorsPolicy.cs
│   │   ├── RegisterRepository.cs
│   │   ├── RegisterServices.cs
│   │   └── RegisterSwagger.cs
│   ├── Middleware/
│   │   └── ExceptionMiddleware.cs
│   ├── Mappers/
│   └── Program.cs
│
├── Reddit-Management-System.Application/  # Application Layer
│   ├── Common/
│   │   ├── MediatR/
│   │   │   ├── ICommand.cs
│   │   │   ├── ICommandHandler.cs
│   │   │   ├── IQuery.cs
│   │   │   └── IQueryHandler.cs
│   │   └── Utilities/
│   ├── Features/
│   │   ├── Email/
│   │   │   └── Command/
│   │   ├── Reddit/
│   │   │   └── Queries/
│   │   ├── Roles/
│   │   ├── Permissions/
│   │   └── Test/
│   ├── Dtos/
│   │   ├── Requests/
│   │   └── Responses/
│   ├── RepositoryInterfaces/
│   │   ├── Reddit/
│   │   │   └── IRedditRepository.cs
│   │   └── Common/
│   ├── ServiceInterfaces/
│   │   ├── Email/
│   │   │   └── IEmailCommandService.cs
│   │   └── Reddit/
│   │       └── IRedditQueryService.cs
│   └── DependencyInjection.cs
│
├── Reddit-Management-System.Service/      # Service Layer
│   ├── Jobs/
│   │   └── RedditEmailJob.cs             # Quartz.NET background job
│   ├── Services/
│   │   ├── Email/
│   │   │   └── EmailCommandService.cs
│   │   ├── Reddit/
│   │   │   └── RedditQueryService.cs
│   │   ├── Roles/
│   │   ├── Permissions/
│   │   └── Users/
│   └── Validators/
│
├── Reddit-Management-System.Repo/         # Repository Layer
│   └── Repositories/
│       ├── Reddit/
│       │   └── RedditRepository.cs
│       ├── Common/
│       │   └── GenericRepository.cs
│       └── BaseRepository.cs
│
├── Reddit-Management-System.Data/         # Infrastructure Layer
│   ├── DbContexts/
│   │   └── ApplicationDbContextWrite.cs
│   ├── Migrations/
│   ├── Configurations/
│   └── Setups/
│       └── DependencyInjection.cs
│
└── Reddit-Management-System.Domain/       # Domain Layer
    ├── Entities/
    │   ├── Users/
    │   ├── Roles/
    │   ├── Permissions/
    │   └── BaseEntity.cs
    ├── Contexts/
    │   ├── IApplicationDbContext.cs
    │   └── IReadDbContext.cs
    └── Interfaces/

```

## 🛠️ Technologies Used

- **.NET 9.0**
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **MediatR** - CQRS implementation
- **Quartz.NET** - Background job scheduling
- **AutoMapper** - Object mapping
- **MailKit** - Email service
- **JWT Authentication**
- **Swagger/OpenAPI**

## 📦 NuGet Packages

```xml
<PackageReference Include="MediatR" Version="14.0.0" />
<PackageReference Include="Quartz" Version="3.x" />
<PackageReference Include="MailKit" Version="4.14.1" />
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.9" />
<PackageReference Include="AutoMapper" Version="x.x.x" />
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="9.0.8" />
<PackageReference Include="Microsoft.Extensions.Caching.StackExchangeRedis" Version="10.0.1" />
```

## ⚙️ Configuration

### appsettings.Development.json
```json
{
  "DbSettings": {
    "DbConnectionString": "User ID=postgres;Password=xxx;Host=localhost;Port=5432;Database=PMS_Db;"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key",
    "Issuer": "JWTMqttBrokerServer",
    "Audience": "JWTMqttBrokerClient",
    "ExpiryMinutes": 60
  },
  "Redis": {
    "ConnectionString": "localhost:6379",
    "InstanceName": "PharmacyManagement:"
  },
  "SmtpSettings": {
    "Host": "smtp.gmail.com",
    "Port": 587,
    "UserName": "your-email@gmail.com",
    "Password": "your-app-password",
    "From": "your-email@gmail.com"
  }
}
```

## 🚀 Getting Started

### Prerequisites
- .NET 9.0 SDK
- PostgreSQL
- Redis (optional)

### Installation

1. Clone the repository
```bash
git clone <repository-url>
cd Reddit
```

2. Update connection strings in `appsettings.Development.json`

3. Run migrations
```bash
dotnet ef database update --project Reddit-Management-System.Data
```

4. Build and run
```bash
dotnet build
dotnet run --project Reddit-Management-System
```

## 📡 API Endpoints

### Reddit Data Fetching
```
GET /api/Test/FetchData
```
- Fetches top 5 posts from r/microsaas and r/SideProject
- Combines and sorts by upvotes
- Returns: List of ScrapResponse

## 🕐 Background Jobs

### RedditEmailJob
- **Schedule**: Daily at 9:00 AM (Cron: `0 0 9 * * ?`)
- **Purpose**: Fetch Reddit posts and send email notifications
- **Configuration**: `RegisterBackgroundJobs.cs`

## 📧 Email Workflow

1. Background job triggers at scheduled time
2. Fetch top posts from Reddit subreddits
3. Process and filter data
4. Generate email content
5. Send to configured recipients

## 🔐 Security Features

- JWT Authentication
- Role-based authorization
- Policy-based access control
- CORS configuration
- Exception middleware

## 📝 Design Patterns Used

- **Clean Architecture**
- **CQRS** (Command Query Responsibility Segregation)
- **Repository Pattern**
- **Dependency Injection**
- **Mediator Pattern**