using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PearlLibrary.Data;
using PearlLibrary.Models;
using PearlLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

// Add user secrets in development and ensure env vars can override configuration.
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true);
}

builder.Configuration.AddEnvironmentVariables();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var connectionString = builder.Configuration.GetConnectionString("LibraryDb");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("Connection string 'LibraryDb' must be configured via environment variables or secret provider.");
}

builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register application services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IBorrowService, BorrowService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

// Add authentication
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? throw new InvalidOperationException("Jwt:Issuer must be configured via environment variables or secret provider.");
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? throw new InvalidOperationException("Jwt:Audience must be configured via environment variables or secret provider.");
var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("Jwt:Key must be configured via environment variables or secret provider.");
if (jwtKey.Length < 32)
{
    throw new InvalidOperationException("Jwt:Key must be at least 32 characters long for sufficient entropy.");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// Add authorization policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Member", policy => policy.RequireRole("Member"));
    options.AddPolicy("Staff", policy => policy.RequireRole("Staff"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
        options.RoutePrefix = "swagger"; // https://localhost:<port>/swagger
    });

    app.UseReDoc(options =>
    {
        options.SpecUrl = "/openapi/v1.json";
        options.RoutePrefix = "redoc"; // https://localhost:<port>/redoc
        options.DocumentTitle = "PearlLibrary API ReDoc";
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/api/auth/login", async ([FromBody] LoginRequest request, [FromServices] IAuthService authService, HttpContext httpContext) =>
{
    var (success, token) = await authService.LoginAsync(request.Username, request.Password);
    if (!success)
    {
        return Results.Unauthorized();
    }

    httpContext.Response.Cookies.Append("auth_token", token, new CookieOptions
    {
        HttpOnly = true,
        Secure = true,
        SameSite = SameSiteMode.Strict,
        Expires = DateTime.UtcNow.AddHours(1)
    });

    return Results.Ok(new { Message = "Login successful" });
})
.WithName("Login");

app.MapPost("/api/auth/signup", async ([FromBody] SignupRequest request, [FromServices] IAuthService authService) =>
{
    var (success, message) = await authService.SignupAsync(request.Username, request.Password, request.Name, request.Email);
    if (!success)
    {
        return Results.BadRequest(message);
    }

    return Results.Ok(new { Message = message });
})
.WithName("Signup");

app.MapPost("/api/users", async ([FromBody] CreateUserRequest request, [FromServices] IAuthService authService) =>
{
    var (success, message) = await authService.CreateUserAsync(request.Username, request.Password, request.Role, request.Name, request.Email);
    if (!success)
    {
        return Results.BadRequest(message);
    }

    return Results.Ok(new { Message = message });
})
.RequireAuthorization("Staff")
.WithName("CreateUser");

app.MapGet("/api/books", async ([FromServices] IBookService bookService, [AsParameters] BookSearchQuery query) =>
{
    var result = await bookService.SearchBooksAsync(query);
    return Results.Ok(result);
})
.RequireAuthorization()
.WithName("GetBooks");

app.MapPost("/api/reservations", async ([FromBody] int bookId, ClaimsPrincipal user, [FromServices] IReservationService reservationService, [FromServices] IAuthService authService) =>
{
    var (success, _) = await authService.ValidateUserAsync(user, out var userId);
    if (!success)
        return Results.Unauthorized();

    var (createSuccess, message, reservationId) = await reservationService.CreateReservationAsync(userId, bookId);
    if (!createSuccess)
        return Results.BadRequest(message);

    var reservation = await reservationService.GetUserReservationsAsync(userId);
    var createdReservation = reservation.FirstOrDefault(r => r.Id == reservationId);

    return Results.Created($"/api/reservations/{reservationId}", new { Id = reservationId, ReservedAt = createdReservation?.ReservedAt });
})
.RequireAuthorization("Member")
.WithName("ReserveBook");

app.MapGet("/api/reservations", async (ClaimsPrincipal user, [FromServices] IReservationService reservationService, [FromServices] IAuthService authService) =>
{
    var (success, _) = await authService.ValidateUserAsync(user, out var userId);
    if (!success)
        return Results.Unauthorized();

    var reservations = await reservationService.GetUserReservationsAsync(userId);
    return Results.Ok(reservations);
})
.RequireAuthorization("Member")
.WithName("GetUserReservations");

app.MapPost("/api/books", async ([FromBody] CreateBookRequest request, [FromServices] IBookService bookService) =>
{
    var book = await bookService.CreateBookAsync(request);
    return Results.Created($"/api/books/{book.Id}", book);
})
.RequireAuthorization("Staff")
.WithName("AddBook");

app.MapPost("/api/reservations/{id}/approve", async (int id, [FromServices] IReservationService reservationService) =>
{
    var (success, message, borrowId, dueDate) = await reservationService.ApproveReservationAsync(id);
    if (!success)
        return Results.BadRequest(message);

    return Results.Ok(new { Id = borrowId, DueDate = dueDate });
})
.RequireAuthorization("Staff")
.WithName("ApproveReservation");

app.MapGet("/api/borrows", async (ClaimsPrincipal user, [FromServices] IBorrowService borrowService, [FromServices] IAuthService authService) =>
{
    var (success, _) = await authService.ValidateUserAsync(user, out var userId);
    if (!success)
        return Results.Unauthorized();

    var borrows = await borrowService.GetUserBorrowsAsync(userId);
    return Results.Ok(borrows);
})
.RequireAuthorization("Member")
.WithName("GetUserBorrows");

app.MapPost("/api/returns/{id}", async (int id, [FromServices] IBorrowService borrowService) =>
{
    var (success, message, borrowId, returnedAt) = await borrowService.ReturnBookAsync(id);
    if (!success)
        return Results.NotFound();

    return Results.Ok(new { Id = borrowId, ReturnedAt = returnedAt });
})
.RequireAuthorization("Staff")
.WithName("ReturnBook");

app.MapGet("/api/dashboard/member", async (ClaimsPrincipal user, [FromServices] IDashboardService dashboardService, [FromServices] IAuthService authService) =>
{
    var (success, _) = await authService.ValidateUserAsync(user, out var userId);
    if (!success)
        return Results.Unauthorized();

    var dashboard = await dashboardService.GetMemberDashboardAsync(userId);
    return Results.Ok(dashboard);
})
.RequireAuthorization("Member")
.WithName("MemberDashboard");

app.MapGet("/api/dashboard/staff", async ([FromServices] IDashboardService dashboardService) =>
{
    var dashboard = await dashboardService.GetStaffDashboardAsync();
    return Results.Ok(dashboard);
})
.RequireAuthorization("Staff")
.WithName("StaffDashboard");

// Seed initial staff user if none exist
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
    var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    await context.Database.MigrateAsync();

    if (!await context.Users.AnyAsync())
    {
        (string hash, byte[] salt) HashPassword(string password)
        {
            using var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);

            var hashBytes = Rfc2898DeriveBytes.Pbkdf2(password, saltBytes, 10000, HashAlgorithmName.SHA256, 32);

            return (Convert.ToBase64String(hashBytes), saltBytes);
        }

        var initialStaffPassword = config["InitialStaff:Password"];
        if (string.IsNullOrWhiteSpace(initialStaffPassword))
        {
            throw new InvalidOperationException("InitialStaff:Password must be configured via user secrets or environment variables and must not be checked into source control.");
        }

        var (hash, salt) = HashPassword(initialStaffPassword);

        var initialStaff = new User
        {
            Username = config["InitialStaff:Username"] ?? "admin",
            PasswordHash = hash,
            Salt = salt,
            Role = UserRole.Staff,
            Name = config["InitialStaff:Name"] ?? "Administrator",
            Email = config["InitialStaff:Email"] ?? "admin@library.com"
        };
        context.Users.Add(initialStaff);
        await context.SaveChangesAsync();
    }
}

app.Run();

record LoginRequest(string Username, string Password);
record SignupRequest(string Username, string Password, string Name, string Email);
record CreateUserRequest(string Username, string Password, UserRole Role, string Name, string Email);
