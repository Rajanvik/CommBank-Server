# CommBank Server

Production-ready backend server and test suite for CommBank.

## 📁 Project Architecture

```text
CommBank-Server/
│
├── 📂 server/                          # ASP.NET Core Web API
│   ├── 📁 Configurations/             # All application configurations & setup modules
│   │   ├── DotEnvLoader.cs            # Environment (.env) loader
│   │   ├── DatabaseSetup.cs           # MongoDB connection & client setup
│   │   ├── DependencyInjection.cs     # Application services registration
│   │   └── MiddlewarePipeline.cs      # CORS, Swagger & HTTP request pipeline
│   ├── 📁 Controllers/                # API Endpoints (Account, Auth, Goal, User, etc.)
│   ├── 📁 Models/                     # Data Models & Schemas
│   ├── 📁 Services/                   # Business Logic & DB queries
│   ├── 📁 Properties/                 # Launch profiles
│   ├── 📄 Program.cs                  # Clean Entry Point (20 lines)
│   ├── 📄 CommBank.csproj             # Project configuration
│   ├── 📄 Secrets.json                # Local secrets template (Git-safe)
│   ├── 📄 appsettings.json            # Base settings
│   └── 📄 appsettings.Development.json# Dev settings
│
├── 📂 tests/                           # Test Suite (xUnit)
│   ├── 📁 Fake/                       # Mock Data
│   ├── 📄 *ControllerTests.cs         # Test files
│   └── 📄 CommBank.Tests.csproj       # Test project file
│
├── 📂 data/                            # Initial Database Seed JSON files
│   ├── Accounts.json
│   ├── Goals.json
│   ├── Tags.json
│   ├── Transactions.json
│   └── Users.json
│
├── 📂 scripts/                         # Database utility scripts
│   └── seed.py                        # Automated database seed script
│
├── 📄 Server.sln                      # Solution file
├── 📄 README.md                       # Documentation
├── 📄 .env                            # Real MongoDB URI (Git-ignored)
└── 📄 .env.example                    # Env template
```

## 🚀 Getting Started

### 1. Environment Setup
```bash
cp .env.example .env
```
Fill in your MongoDB credentials in `.env`.

### 2. Seed Database
To seed the initial sample data into MongoDB Atlas:
```bash
python3 scripts/seed.py
```

### 3. Run Server
```bash
dotnet run --project server
```

### 4. Run Tests
```bash
dotnet test tests
```
