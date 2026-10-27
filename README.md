# CommBank Server

Production-ready backend server and test suite for CommBank.

## 📁 Project Architecture

Clear separation between the API Server and Test Suite:

```text
CommBank-Server/
├── server/                            # Backend API Server (ASP.NET Core)
│   ├── Controllers/                   # API Endpoints (Account, Auth, Goal, User, etc.)
│   ├── Models/                        # Data models & schemas
│   ├── Services/                      # Business logic & Mongo services
│   ├── Properties/                    # Launch profiles & configuration
│   ├── Program.cs                     # Server bootstrap & DI setup
│   ├── Secrets.json                   # Secrets configuration
│   ├── appsettings.json               # Application settings
│   ├── appsettings.Development.json   # Dev environment settings
│   └── CommBank.csproj                # Server project file
│
├── tests/                             # Test Suite (xUnit)
│   ├── Fake/                          # Mock services & test fixtures
│   ├── AccountControllerTests.cs      # Account controller test cases
│   ├── AuthControllerTests.cs         # Auth controller test cases
│   ├── GoalControllerTests.cs         # Goal controller test cases
│   ├── TagControllerTests.cs          # Tag controller test cases
│   ├── TransactionControllerTests.cs  # Transaction controller test cases
│   ├── UserControllerTests.cs         # User controller test cases
│   ├── Usings.cs                      # Global test usings
│   └── CommBank.Tests.csproj          # Tests project file
│
├── Server.sln                         # Solution file connecting server & tests
├── .env                               # Local database credentials (Git-ignored)
├── .env.example                       # Environment template
└── .gitignore                         # Git exclusion rules
```

## 🚀 Getting Started

### 1. Environment Setup
```bash
cp .env.example .env
```

### 2. Run Server
```bash
dotnet run --project server
```

### 3. Run Tests
```bash
dotnet test tests
```
