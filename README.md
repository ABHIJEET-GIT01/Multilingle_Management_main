# WebAPI NOVO Assignment

A comprehensive ASP.NET Core 10 Web API project featuring user authentication, role-based authorization, JWT token management, and multilingual support for forms and messages.

## Table of Contents

- [Features](#features)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
  - [Windows Setup](#windows-setup)
  - [Linux/Mac Setup](#linuxmac-setup)
- [Database Setup](#database-setup)
- [Configuration](#configuration)
- [Running the Project](#running-the-project)
- [API Documentation](#api-documentation)
- [Project Structure](#project-structure)
- [Default Credentials](#default-credentials)
- [Troubleshooting](#troubleshooting)

## Features

- **User Authentication**: JWT-based authentication with access and refresh tokens
- **Role-Based Authorization**: Admin and User roles with permission management
- **User Management**: CRUD operations for users with role assignments
- **Role Management**: Create and manage system roles
- **Multilingual Support**: Support for English, Hindi, and Marathi languages
- **Dynamic Forms**: Localized form structures for different languages
- **Token Refresh**: Secure token refresh mechanism with revocation support
- **API Documentation**: Interactive Scalar UI for API exploration

## Prerequisites

### System Requirements

- **Operating System**: Windows 10/11, Linux, or macOS
- **RAM**: Minimum 4GB (8GB recommended)
- **Disk Space**: Minimum 2GB

### Required Software

1. **SDK**: .NET 10.0 SDK or later
   - Download from: https://dotnet.microsoft.com/download/dotnet/10.0

2. **Database**: Microsoft SQL Server
   - Windows: SQL Server 2019 or later (Express, Standard, or Enterprise)
   - Linux: SQL Server 2019+ in Docker container
   - macOS: SQL Server in Docker container

3. **Code Editor** (Optional):
   - Visual Studio 2026 (Recommended)
   - Visual Studio Code with C# Dev Kit extension
   - JetBrains Rider

4. **Git** (for version control)
   - Download from: https://git-scm.com/

## Installation

### Windows Setup

#### 1. Install Prerequisites

```bash
# Check if .NET SDK is installed
dotnet --version

# If not installed, download and install from https://dotnet.microsoft.com/download/dotnet/10.0
```

#### 2. Clone the Repository

```bash
git clone https://github.com/Abhijeet077/WebAPI_NOVOAssignment.git
cd WebAPI_NOVOAssignment
```

#### 3. Restore NuGet Packages

```bash
dotnet restore
```

#### 4. Set Up SQL Server Database

**Option A: Using SQL Server Management Studio (SSMS)**

1. Open SQL Server Management Studio
2. Connect to your SQL Server instance
3. Create a new database:

```sql
CREATE DATABASE [WebAPI_NOVO_DB]
GO
```

4. Verify the database was created

**Option B: Using Command Line**

```bash
# Using SQL Server command-line tools (if installed)
sqlcmd -S "SERVER_NAME" -U "sa" -P "YOUR_PASSWORD"
> CREATE DATABASE [WebAPI_NOVO_DB]
> GO
> EXIT
```

#### 5. Update Configuration

Edit `appsettings.json` and update the connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=YOUR_SERVER_NAME;Initial Catalog=WebAPI_NOVO_DB;Integrated Security=True;Encrypt=False"
  }
}
```

**Connection String Examples:**

- **Local with Windows Authentication**:
  ```
  Data Source=.;Initial Catalog=WebAPI_NOVO_DB;Integrated Security=True;Encrypt=False
  ```
  
- **Local with SQL Server Authentication**:
  ```
  Data Source=.;Initial Catalog=WebAPI_NOVO_DB;User Id=sa;Password=YourPassword;Encrypt=False
  ```
  
- **Remote Server**:
  ```
  Data Source=SERVER_IP_OR_NAME;Initial Catalog=WebAPI_NOVO_DB;User Id=sa;Password=YourPassword;Encrypt=False
  ```

#### 6. Apply Database Migrations

```bash
# Navigate to project directory
cd WebAPI_NOVOAssignment

# Apply migrations to create database schema
dotnet ef database update
```

#### 7. Run the Application

```bash
dotnet run
```

The application will start and automatically seed the database with default roles (Admin, User) and a default admin user.

---

### Linux/Mac Setup

#### 1. Install Prerequisites

**Ubuntu/Debian:**
```bash
# Install .NET SDK
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x ./dotnet-install.sh
./dotnet-install.sh --channel 10.0

# Add to PATH
export PATH=$PATH:$HOME/.dotnet
echo 'export PATH=$PATH:$HOME/.dotnet' >> ~/.bashrc
source ~/.bashrc
```

**macOS (using Homebrew):**
```bash
brew install dotnet-sdk
```

#### 2. Set Up SQL Server with Docker

```bash
# Pull SQL Server image
docker pull mcr.microsoft.com/mssql/server:2022-latest

# Run SQL Server container
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=YourPassword@123" \
  -p 1433:1433 \
  --name sqlserver \
  -d mcr.microsoft.com/mssql/server:2022-latest

# Wait for container to be ready (30-60 seconds)
sleep 30
```

#### 3. Create Database

```bash
# Install SQL Server tools if needed
# Ubuntu/Debian:
curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
sudo add-apt-repository "$(curl https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/mssql-server-2022.list)"
sudo apt-get update
sudo apt-get install -y mssql-tools18

# Create database
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P 'YourPassword@123' -C -Q 'CREATE DATABASE [WebAPI_NOVO_DB]'
```

#### 4. Clone and Configure

```bash
# Clone repository
git clone https://github.com/Abhijeet077/WebAPI_NOVOAssignment.git
cd WebAPI_NOVOAssignment

# Restore packages
dotnet restore

# Edit appsettings.json with appropriate connection string
# Linux/Mac connection string example:
# "DefaultConnection": "Data Source=localhost;Initial Catalog=WebAPI_NOVO_DB;User Id=sa;Password=YourPassword@123;Encrypt=False;TrustServerCertificate=True"
```

#### 5. Apply Migrations and Run

```bash
cd WebAPI_NOVOAssignment

# Apply migrations
dotnet ef database update

# Run the application
dotnet run
```

---

## Database Setup

### Database Schema

The application creates three main tables:

#### **Users Table**
```sql
- Id (GUID, Primary Key)
- Username (VARCHAR(20), Unique, Required)
- Email (VARCHAR(256), Unique, Required)
- PasswordHash (NVARCHAR(MAX), Required)
- FirstName (VARCHAR(100))
- LastName (VARCHAR(100))
- CreatedDate (DateTime)
- UpdatedDate (DateTime)
```

#### **Roles Table**
```sql
- Id (GUID, Primary Key)
- Name (VARCHAR(50), Unique, Required)
- Description (VARCHAR(500))
- CreatedDate (DateTime)
```

#### **UserRoles Junction Table**
```sql
- UsersId (GUID, Foreign Key)
- RolesId (GUID, Foreign Key)
```

#### **RefreshTokens Table**
```sql
- Id (GUID, Primary Key)
- Token (NVARCHAR(MAX), Required)
- UserId (GUID, Foreign Key)
- ExpiryDate (DateTime)
- IsRevoked (BIT, Default: 0)
```

### Seed Data

The database is automatically seeded with:

**Default Roles:**
- **Admin**: Administrator with full system access
- **User**: Regular user with limited permissions

### Manual Database Operations

```bash
# Create a new migration after model changes
dotnet ef migrations add MigrationName

# View migration status
dotnet ef migrations list

# Revert to a previous migration
dotnet ef database update PreviousMigrationName

# Drop the database (for development only)
dotnet ef database drop

# Recreate database
dotnet ef database update
```

---

## Configuration

### appsettings.json Structure

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=YOUR_SERVER;Initial Catalog=WebAPI_NOVO_DB;Integrated Security=True;Encrypt=False"
  },
  "JwtSettings": {
    "SecretKey": "your-super-secret-key-at-least-32-characters-long",
    "Issuer": "WebAPI_NOVOAssignment",
    "Audience": "WebAPI_NOVOAssignment_Users",
    "AccessTokenExpirationMinutes": 15,
    "RefreshTokenExpirationDays": 1
  }
}
```

### Important Configuration Notes

1. **JWT SecretKey**: 
   - Must be at least 32 characters long
   - Change in production to a secure, random string
   - Keep it secret and do not commit to version control

2. **Database Connection**:
   - Update with your server details
   - Use Windows Authentication when possible for security
   - In production, use encrypted connections (Encrypt=True)

3. **CORS Settings**:
   - Currently set to "AllowAll"
   - Restrict origins in production (see Program.cs for example)

---

## Running the Project

### Start the Application

```bash
# From the project root
cd WebAPI_NOVOAssignment
dotnet run
```

The application will:
1. Apply pending migrations (if any)
2. Seed default data
3. Start listening on `http://localhost:5000` and `https://localhost:5001`

### Access the API

- **API Root**: http://localhost:5000 or https://localhost:5001
- **Scalar UI**: http://localhost:5000/scalar
- **OpenAPI Spec**: http://localhost:5000/openapi/v1.json

### Stop the Application

Press `Ctrl+C` in the terminal

---

## API Documentation

### Interactive Documentation

Access the Scalar UI at `http://localhost:5000/scalar` for an interactive API explorer with the ability to:
- View all available endpoints
- Test API calls
- See request/response examples
- Download OpenAPI specification

### Authentication Flow

1. **Login**: POST `/api/auth/login` with credentials
2. **Receive**: Access token (expires in 15 minutes) and refresh token (expires in 1 day)
3. **Use**: Include access token in Authorization header: `Bearer {token}`
4. **Refresh**: POST `/api/auth/refresh-token` to get a new access token

### Key Endpoints

**Authentication:**
- `POST /api/auth/login` - User login
- `POST /api/auth/refresh-token` - Refresh access token
- `POST /api/auth/logout` - Logout and revoke token

**Users:**
- `GET /api/users` - List all users (Admin only)
- `POST /api/users` - Create new user
- `GET /api/users/{id}` - Get user details
- `PUT /api/users/{id}` - Update user
- `DELETE /api/users/{id}` - Delete user

**Roles:**
- `GET /api/roles` - List all roles
- `POST /api/roles` - Create new role
- `GET /api/roles/{id}` - Get role details
- `PUT /api/roles/{id}` - Update role
- `DELETE /api/roles/{id}` - Delete role

**Forms:**
- `GET /api/forms/{formType}?language={lang}` - Get form structure

---

## Project Structure

```
WebAPI_NOVOAssignment/
│
├── Controllers/              # API endpoints
│   ├── AuthController.cs
│   ├── UsersController.cs
│   ├── RolesController.cs
│   └── FormsController.cs
│
├── Services/                 # Business logic
│   ├── Interfaces/
│   │   ├── IAuthService.cs
│   │   ├── IUserService.cs
│   │   └── IRoleService.cs
│   ├── AuthService.cs
│   ├── UserService.cs
│   ├── RoleService.cs
│   ├── LocalizationService.cs
│   └── ...
│
├── Models/                   # Database entities
│   ├── User.cs
│   ├── Role.cs
│   └── RefreshToken.cs
│
├── DTOs/                     # Data transfer objects
│   ├── LoginRequestDto.cs
│   ├── UserDetailDto.cs
│   ├── CreateUserRequestDto.cs
│   └── ...
│
├── Repositories/             # Data access layer
│   ├── UserRepository.cs
│   ├── RoleRepository.cs
│   └── RefreshTokenRepository.cs
│
├── Data/
│   └── ApplicationDbContext.cs  # EF Core DbContext
│
├── Resources/
│   └── Localization/         # Multilingual resources
│       ├── messages.en.json
│       ├── messages.hi.json
│       ├── messages.mr.json
│       ├── forms.en.json
│       ├── forms.hi.json
│       └── forms.mr.json
│
├── Utilities/
│   ├── JwtTokenGenerator.cs
│   ├── PasswordHasher.cs
│   ├── FormStructureProvider.cs
│   ├── RegisterServices.cs
│   └── ...
│
├── Extensions/
│   └── HttpContextExtensions.cs
│
├── Program.cs               # Application startup
├── appsettings.json         # Configuration
└── WebAPI_NOVOAssignment.csproj
```

---

## Default Credentials
⚠️ **IMPORTANT**: I added credentials for initial access and testing as part of the assignment. This is not a good practice for production systems.
After database setup, use these credentials to login:

| Field | Value |
|-------|-------|
| Username | `admin` |
| Email | `admin@novo.com` |
| Password | `Admin@123456` |
| Role | Admin |

### First Login Steps:
1. Go to `/scalar` (API explorer)
2. POST to `/api/auth/login` with credentials
3. Copy the `accessToken` from response
4. Click "Auth" button and paste the token
5. Now you can make authenticated requests

⚠️ **IMPORTANT**: Change the default password immediately in production

---

## Troubleshooting

### Common Issues

#### 1. Database Connection Failed

**Error**: `Cannot connect to database`

**Solution**:
```bash
# Verify SQL Server is running
# Windows: Check Services or SQL Server Configuration Manager
# Linux/Docker: Check container status
docker ps -a | grep sqlserver

# Update connection string in appsettings.json
# Verify server name and credentials
# Ensure database exists and is accessible
```

#### 2. Migrations Not Applied

**Error**: `The model backing the 'ApplicationDbContext' context has changed, but the database has not been updated.`

**Solution**:
```bash
# Update database
dotnet ef database update

# Or recreate database
dotnet ef database drop -f
dotnet ef database update
```

#### 3. Port Already in Use

**Error**: `Cannot bind to port 5000/5001`

**Solution**:
```bash
# Run on different ports
dotnet run --urls "http://localhost:5002;https://localhost:5003"

# Or kill process using the port
# Windows:
netstat -ano | findstr :5000
taskkill /PID <PID> /F

# Linux/Mac:
lsof -i :5000
kill -9 <PID>
```

#### 4. JWT Token Expired

**Error**: `Token expired` when making requests

**Solution**:
```bash
# Use the refresh token endpoint
# POST /api/auth/refresh-token with refresh token
# This will give you a new access token
```

#### 5. SSL/HTTPS Certificate Issue

**Error**: `Cannot connect via HTTPS`

**Solution**:
```bash
# Generate development certificate
dotnet dev-certs https --clean
dotnet dev-certs https --trust
```

---

## Production Deployment Checklist

- [ ] Change default admin password
- [ ] Update JWT SecretKey to a secure, random value (32+ characters)
- [ ] Set `Encrypt=True` in connection string
- [ ] Update CORS policy to allow specific origins only
- [ ] Enable HTTPS only
- [ ] Use SQL Server Authentication or Windows Authentication (not both)
- [ ] Enable logging and monitoring
- [ ] Backup database regularly
- [ ] Implement API rate limiting
- [ ] Use environment-specific appsettings files

---

## Support

For issues, questions, or contributions:
- GitHub: https://github.com/Abhijeet077/WebAPI_NOVOAssignment
- Create an issue on GitHub repository

## License

This project is provided as-is for educational and assignment purposes.

---

**Last Updated**: 2024
**Framework**: ASP.NET Core 10
**Database**: SQL Server 2019+
