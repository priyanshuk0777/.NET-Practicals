# Practical 10 - Student Management REST API using ASP.NET Core 8

A complete Web API built with **ASP.NET Core (.NET 8)** that manages students with full
**CRUD operations**, a **custom request-logging middleware**, and a clean **layered
project structure**. Data is stored in an **in-memory list**, so the project runs
immediately with **no database setup required**.

---

## 1. Technologies Used

| Component          | Technology                          |
|--------------------|-------------------------------------|
| Framework          | ASP.NET Core Web API (.NET 8)       |
| IDE                | Visual Studio 2022 (17.8 or later)  |
| API documentation  | Swagger / OpenAPI (Swashbuckle)     |
| Validation         | Data Annotations                    |
| Data storage       | In-memory list (singleton service)  |
| API testing        | Swagger UI + Postman collection     |

---

## 2. How to Run in Visual Studio 2022

1. **Extract** the ZIP to any folder, e.g. `C:\Practicals\StudentManagement`.
2. **Double-click** `StudentManagement.sln` (or open it via
   *File > Open > Project/Solution* inside Visual Studio).
3. Wait for NuGet packages to restore automatically (the Swashbuckle package is
   restored on first build; an internet connection is needed once).
4. Press **F5** (or the green **https** run button in the toolbar).
5. The browser opens Swagger UI at `https://localhost:7017/swagger`.
   If a certificate-trust dialog appears, click **Yes** to trust the
   ASP.NET Core development certificate.
6. Test every endpoint directly from Swagger, or import the included
   `StudentManagement.postman_collection.json` into Postman.

**Run without Visual Studio (optional):**

```bash
cd StudentManagement.API
dotnet run --launch-profile https
```

---

## 3. Project Structure (and why it is organized this way)

```
StudentManagement/
├── StudentManagement.sln                  # Solution - groups all projects
├── StudentManagement.postman_collection.json
├── .gitignore
└── StudentManagement.API/                 # Web API project
    ├── StudentManagement.API.csproj       # Project file (net8.0, Swashbuckle)
    ├── Program.cs                         # App entry point: DI + middleware pipeline
    ├── appsettings.json                   # Configuration (logging levels)
    ├── Properties/
    │   └── launchSettings.json            # Dev ports / launch profiles (Kestrel, IIS Express)
    ├── Controllers/
    │   └── StudentsController.cs          # HTTP layer: routing, status codes, DTO mapping
    ├── Middleware/
    │   └── RequestLoggingMiddleware.cs    # Custom middleware: logs every request/response
    ├── Models/
    │   ├── Student.cs                     # Core entity (never exposed to clients)
    │   └── StudentDtos.cs                 # Create/Update/Response DTOs + validation rules
    └── Services/
        ├── IStudentService.cs             # Abstraction the controller depends on
        └── StudentService.cs              # In-memory CRUD implementation + seed data
```

**Why layers instead of everything in Program.cs?**

- **Controllers** only deal with HTTP concerns (routes, status codes) - no business
  or data logic, keeping the API surface thin.
- **Services** hold the CRUD logic, so it can be reused by any controller and
  replaced later (e.g. by an EF Core database service) without touching the
  controller code.
- **Models/DTOs** separate what is *stored* (`Student`) from what is *sent/received*
  over HTTP (DTOs), so clients can never set internal fields like `Id` or
  `EnrolledOn`, and validation rules live in one place.

---

## 4. Middleware Pipeline

Middleware are software components assembled into a pipeline that processes every
HTTP request and response. Each component chooses whether to pass the request to the
next component and can perform work both **before** and **after** it.

```
HTTP Request
   │
   ▼
UseHttpsRedirection        (built-in - redirects http to https)
   │
   ▼
UseRequestLogging          (custom - logs method, path, status code, elapsed ms)
   │
   ▼
MapControllers / Routing   (matches the URL to a controller action)
   │
   ▼
StudentsController action  (runs CRUD logic via IStudentService)
   │
   ▼
Response travels back through the same chain in reverse order
```

The **custom `RequestLoggingMiddleware`** demonstrates the classic middleware
pattern:

- Code **before** `_next(context)` executes on the request side.
- `await _next(context)` calls the next component in the pipeline.
- Code **after** `_next(context)` executes on the response side - here it measures
  total elapsed time with `Stopwatch` and logs the final status code.

Watch the Visual Studio **Output / console window** while testing - every request
produces two log lines like:

```
info: StudentManagement.API.Middleware.RequestLoggingMiddleware[0]
      --> Request: GET /api/students
info: StudentManagement.API.Middleware.RequestLoggingMiddleware[0]
      <-- Response: GET /api/students => 200 in 12 ms
```

**Order matters:** if `UseRequestLogging()` were placed after `MapControllers()`
it would never run; middleware registered first wraps everything registered after it.

---

## 5. API Endpoints

Base URL: `https://localhost:7017` (https profile) or `http://localhost:5217` (http profile)

| Method | Endpoint           | Description                | Success | Not Found |
|--------|--------------------|----------------------------|---------|-----------|
| GET    | `/api/students`    | Get all students           | 200 OK  | -         |
| GET    | `/api/students/1`  | Get student by id          | 200 OK  | 404       |
| POST   | `/api/students`    | Create a new student       | 201 Created + Location header | - |
| PUT    | `/api/students/1`  | Update an existing student | 200 OK  | 404       |
| DELETE | `/api/students/1`  | Delete a student           | 204 No Content | 404 |

The route `api/[controller]` resolves to `api/students` because the controller is
named `StudentsController`.

---

## 6. Sample Requests and Responses

**POST `https://localhost:7017/api/students`**

Request body (JSON):

```json
{
  "name": "Sneha Iyer",
  "email": "sneha.iyer@example.com",
  "course": "B.Tech AI/ML",
  "age": 22
}
```

Response - **201 Created**:

```json
{
  "id": 4,
  "name": "Sneha Iyer",
  "email": "sneha.iyer@example.com",
  "course": "B.Tech AI/ML",
  "age": 22,
  "enrolledOn": "2026-10-28T10:15:30.123Z"
}
```

**Validation example** - send an invalid body such as a bad email and age 5:

```json
{
  "name": "X",
  "email": "not-an-email",
  "course": "",
  "age": 5
}
```

Response - **400 Bad Request** (generated automatically by `[ApiController]` from the
Data Annotation rules in `StudentDtos.cs` - no manual checks were written):

```json
{
  "type": "https://tools.ietf.org/html/rfc9110#section-15.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "Email": ["Invalid email address format."],
    "Name": ["Name must be between 2 and 60 characters."],
    "Age": ["Age must be between 15 and 80."]
  }
}
```

**DELETE** returns empty body with **204 No Content**; a repeat DELETE of the same id
returns **404** with `{ "message": "Student with id 2 was not found." }`.

---

## 7. Testing with Postman

1. Open Postman > **Import** > drag `StudentManagement.postman_collection.json` in.
2. The collection variable `baseUrl` is pre-set to `https://localhost:7017`.
3. Start the API in Visual Studio first, then run the requests in order:
   Get All > Get By Id > Create > Update > Delete.

---

## 8. Key Concepts Demonstrated (viva pointers)

- **Middleware pipeline** - custom `RequestLoggingMiddleware`, built-in
  `UseHttpsRedirection`, correct ordering, and the `_next` delegate.
- **REST API** - controller actions mapped to GET/POST/PUT/DELETE, proper status
  codes (200, 201, 204, 400, 404) and `CreatedAtAction` Location header.
- **Project structure** - layered folders (Controllers / Services / Models /
  Middleware) with separation of concerns.
- **Dependency Injection** - `IStudentService` registered as a singleton and
  injected through the controller constructor.
- **DTO pattern** - request DTOs (create/update) are decoupled from the stored
  entity and the response DTO.
- **Model validation** - Data Annotations (`[Required]`, `[StringLength]`,
  `[EmailAddress]`, `[Range]`) enforced automatically by `[ApiController]`.
- **Thread-safe in-memory store** - a lock guards the shared list because a
  singleton service is used by concurrent requests.
