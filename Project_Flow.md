**Project Flow**

==============



Program.cs

&#x20;    │

&#x20;    ▼

CreateBuilder()

&#x20;    │

&#x20;    ▼

Register Services

&#x20;    │

&#x20;    ├── Controllers

&#x20;    ├── Swagger

&#x20;    └── Other services

&#x20;    │

&#x20;    ▼

Build()

&#x20;    │

&#x20;    ▼

Configure Middleware

&#x20;    │

&#x20;    ├── HTTPS

&#x20;    ├── Authentication

&#x20;    ├── Authorization

&#x20;    └── etc.

&#x20;    │

&#x20;    ▼

Map Controllers

&#x20;    │

&#x20;    ▼

app.Run()

&#x20;    │

&#x20;    ▼

HTTP Requests

&#x20;    │

&#x20;    ▼

Controllers





================Completed Till 03/October/2026=============





✅ ASP.NET Core Web API

✅ Dependency Injection

✅ Interface + Service

✅ EF Core

✅ SQL Server / LocalDB

✅ Migrations

✅ GET all tickets

✅ GET ticket by ID

✅ POST ticket

✅ PUT ticket

✅ DELETE ticket

✅ DTOs

✅ DTO validation

✅ async/await + EF Core async operations

==================From 03/October/2026 Started ================



**Global Exception Handling → Logging → standardized error response**



**| Level         | Use it for                           | Example                    |**

**| ------------- | ------------------------------------ | -------------------------- |**

**| `Trace`       | Very detailed diagnostic information | Internal execution details |**

**| `Debug`       | Developer debugging information      | SQL/query debugging        |**

**| `Information` | Normal application activity          | Ticket created             |**

**| `Warning`     | Something unexpected but recoverable | Ticket not found           |**

**| `Error`       | An operation failed                  | Database exception         |**

**| `Critical`    | Serious application/system failure   | Application cannot start   |**





Then:



**Unit Testing → Repository/query improvements → Authentication/JWT → RBAC → Git/GitHub → CI/CD → Azure → React → GenAI/RAG → LINE integration.**





**======================Unit Testing=========================================**



So after this, our service tests will cover:



✅ Create — successful creation

✅ Get by ID — existing ticket

✅ Get by ID — missing ticket

✅ Update — existing ticket

✅ Update — missing ticket

✅ Delete — existing ticket

✅ Delete — missing ticket





==========================**Git/GitHub**=======================================

You write code

&#x20;     ↓

Git commit

&#x20;     ↓

GitHub

&#x20;     ↓

GitHub Actions

&#x20;     ↓

Restore → Build → Test

&#x20;     ↓

Azure

&#x20;     ↓

Deploy API

