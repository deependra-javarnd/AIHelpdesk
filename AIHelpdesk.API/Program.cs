using AIHelpdesk.API.Data;
using AIHelpdesk.API.Exceptions;
using AIHelpdesk.API.Interfaces;
using AIHelpdesk.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ===============================
// SERVICE REGISTRATION
// ===============================

//builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
//builder.Services.AddProblemDetails();

//builder.Services.AddExceptionHandler<GlobalExceptionHandlerTest>();
//builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandlerTest2>();
builder.Services.AddProblemDetails();

// Add services to the container.
builder.Services.AddControllers();

//For CORS error on browser when .Net APIs and and React running on different port.
builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactPolicy", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

//For SQL DB COnnection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

//For DI registration of Ticket service and ticket interface to use in ticket controller.
builder.Services.AddScoped<ITicketService, TicketService>();
//For Auth
builder.Services.AddScoped<IAuthService, AuthService>();

//Factory selects implementation Example of Dynamically picking out of both (Email and SMS derived class of Inotification interface)

builder.Services.AddScoped<EmailNotificationService>();
builder.Services.AddScoped<SMSNotificationService>();

builder.Services.AddScoped<Func<string, INotification>>(provider => type =>
    type.ToLower() switch
    {
        "email" => provider.GetRequiredService<EmailNotificationService>(),
        "sms" => provider.GetRequiredService<SMSNotificationService>(),
        _ => throw new ArgumentException("Invalid Notification Type")

    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter your JWT token."
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var jwtSettings = builder.Configuration.GetSection("Jwt");

var jwtKey = jwtSettings["Key"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],

            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtKey!))
        };
    });

builder.Services.AddAuthorization();
//var passwordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123")// way to hash user entered password and save in to DB;
//Console.WriteLine("HASH:");
//Console.WriteLine(passwordHash);

// ===============================
// MIDDLEWARE PIPELINE STARTS
// ===============================

var app = builder.Build();
app.UseExceptionHandler();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseCors("ReactPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

// ===============================
// APPLICATION STARTS
// ===============================
app.Run();
