using StudentManagement.API.Middleware;
using StudentManagement.API.Services;

var builder = WebApplication.CreateBuilder(args);

// ------------------------------------------------------------------
// STEP 1: Register services in the Dependency Injection (DI) container
// ------------------------------------------------------------------
builder.Services.AddControllers();          // Discovers and registers API controllers
builder.Services.AddEndpointsApiExplorer(); // Required for Swagger / OpenAPI
builder.Services.AddSwaggerGen();           // Generates the Swagger document

// In-memory data store service (no database required for this practical).
// Singleton = one instance shared by every HTTP request for the app lifetime.
builder.Services.AddSingleton<IStudentService, StudentService>();

var app = builder.Build();

// ------------------------------------------------------------------
// STEP 2: Configure the HTTP request pipeline (middleware order matters!)
//
// Request flow:
//   Client -> HTTPS Redirection -> Request Logging -> Routing -> Endpoint
// Response flows back through the same chain in reverse order.
// ------------------------------------------------------------------
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();    // Serves the OpenAPI JSON at /swagger/v1/swagger.json
    app.UseSwaggerUI();  // Serves the interactive Swagger UI at /swagger
}

app.UseHttpsRedirection();           // Built-in middleware: http -> https
app.UseRequestLogging();             // Custom middleware (see /Middleware folder)
app.MapControllers();                // Endpoint routing to controller actions

app.Run();
