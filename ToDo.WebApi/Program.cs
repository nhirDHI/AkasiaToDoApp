using Microsoft.OpenApi.Models;
using Serilog;
using ToDo.Application;
using ToDo.Data;
using ToDo.Identity;
using ToDo.Infrastructure;
using ToDo.WebApi.Middleware;
using ToDo.WebApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configure the logging system to use Serilog
builder.Host.UseSerilog((context, loggerConfig) => loggerConfig
    //.WriteTo.Console() // Log output to the console
    .ReadFrom.Configuration(context.Configuration)); // Read logging configuration from appsettings.json

// Register application, persistence, and infrastructure services with the DI container
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddDataServices(builder.Configuration);



builder.Services.AddControllers();
// Read CORS settings from appsettings.json
var corsSettings = builder.Configuration.GetSection("CorsSettings").Get<CorsSettings>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        if (corsSettings.AllowedOrigins == "*")
        {
            // Allow any origin
            builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
        else
        {
            // Allow specific origins
            builder.WithOrigins(corsSettings.AllowedOrigins.Split(","))
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        }
    });
});


// Add the HttpContextAccessor to enable access to the HttpContext in services
builder.Services.AddHttpContextAccessor();

// Register services for Swagger/OpenAPI documentation generation
builder.Services.AddEndpointsApiExplorer(); // Add support for API endpoint exploration

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(swagger =>
{
    // Generate default UI for Swagger Documentation
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1", // API version
        Title = "JWT Token Authentication API", // API title
        Description = ".NET 8 Web API" // API description
    });

    // Define security scheme for JWT authorization in Swagger
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization", // Header name
        Type = SecuritySchemeType.ApiKey, // Type of security
        Scheme = "Bearer", // Authorization scheme
        BearerFormat = "JWT", // Format of the bearer token
        In = ParameterLocation.Header, // Location of the token (in header)
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"", // Description for users
    });

    // Add security requirements for the Swagger documentation
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme, // Type of reference
                    Id = "Bearer" // ID of the security scheme
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Use custom exception handling middleware
app.UseMiddleware<ExceptionMiddleware>();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseSwagger(); // Enable Swagger documentation
app.UseSwaggerUI(); // Enable Swagger UI for better visualization

app.UseSerilogRequestLogging(); // Log HTTP requests using Serilog

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins"); // Enable CORS for all requests

app.UseAuthentication(); // Enable authentication middleware
app.UseAuthorization(); // Enable authorization middleware

app.MapControllers(); // Map attribute-routed controllers

app.Run();
