# CommBank Server

Production-ready backend server and test suite for CommBank.

## 📁 Project Architecture

```text
CommBank-Server/
│
├── 📂 server/                          # ASP.NET Core Web API
│   ├── 📁 Configurations/             # Setup & configuration modules
│   │   ├── DotEnvLoader.cs            # Environment (.env) loader
│   │   ├── DatabaseSetup.cs           # MongoDB connection & client setup
│   │   ├── DependencyInjection.cs     # Application services registration
│   │   └── MiddlewarePipeline.cs      # CORS, Swagger & HTTP request pipeline
│   │
│   ├── 📁 Controllers/                # HTTP API Routers (Domain Folders)
│   │   ├── 📁 Accounts/
│   │   │   └── AccountController.cs   # Exposes /api/account
│   │   ├── 📁 Auth/
│   │   │   └── AuthController.cs      # Exposes /api/auth
│   │   ├── 📁 Goals/
│   │   │   └── GoalController.cs      # Exposes /api/goal
│   │   ├── 📁 Tags/
│   │   │   └── TagController.cs       # Exposes /api/tag
│   │   ├── 📁 Transactions/
│   │   │   └── TransactionController.cs # Exposes /api/transaction
│   │   └── 📁 Users/
│   │       └── UserController.cs      # Exposes /api/user
│   │
│   ├── 📁 Models/                     # Core Database Models (Pure ERD Entities)
│   │   ├── Account.cs                 # Account Model + AccountType Enum
│   │   ├── Goal.cs                    # Goal Model
│   │   ├── Tag.cs                     # Tag Model
│   │   ├── Transaction.cs             # Transaction Model + TransactionType Enum
│   │   └── User.cs                    # User Model
│   │
│   ├── 📁 Services/                   # Domain Services (Queries & Mutations)
│   │   ├── 📁 Accounts/
│   │   │   ├── IAccountsService.cs    # Contract
│   │   │   ├── AccountsQuery.cs       # GET Operations (Read Data)
│   │   │   └── AccountsMutation.cs    # POST / PUT / DELETE Operations (Write Data)
│   │   ├── 📁 Auth/
│   │   │   ├── IAuthService.cs
│   │   │   └── AuthMutation.cs        # Login & Auth Mutations
│   │   ├── 📁 Goals/
│   │   │   ├── IGoalsService.cs
│   │   │   ├── GoalsQuery.cs          # GET Operations
│   │   │   └── GoalsMutation.cs       # POST / PUT / DELETE Operations
│   │   ├── 📁 Tags/
│   │   │   ├── ITagsService.cs
│   │   │   ├── TagsQuery.cs           # GET Operations
│   │   │   └── TagsMutation.cs        # POST / PUT / DELETE Operations
│   │   ├── 📁 Transactions/
│   │   │   ├── ITransactionsService.cs
│   │   │   ├── TransactionsQuery.cs   # GET Operations
│   │   │   └── TransactionsMutation.cs# POST / PUT / DELETE Operations
│   │   └── 📁 Users/
│   │       ├── IUsersService.cs
│   │       ├── UsersQuery.cs          # GET Operations
│   │       └── UsersMutation.cs       # POST / PUT / DELETE Operations
│   │
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
├── 📂 docs/                            # Comparison & architecture guides
│   ├── NEXTJS_PRISMA_VS_CSHARP_DOTNET.md
│   └── NEXTJS_PRISMA_VS_CSHARP_DOTNET.pdf
│
├── 📂 scripts/                         # Utility scripts
│   ├── seed.py                        # Automated database seed script
│   └── generate_pdf.py                # Guide PDF generator
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

### 2. Seed Database
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
