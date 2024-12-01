using MedReminder.Api.Services;
using MedReminder.Api.Services.Interfaces;
using MedReminder.Dal.Dao;
using MedReminder.Dal.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Load user secrets in development
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>();
}

// Get connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                          ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");

// Register services
builder.Services.AddSingleton<ISmsService, TwilioSmsService>();


builder.Services.AddScoped<IMedicationDao>(_ => new MedicationDao(connectionString));
builder.Services.AddScoped<IUserDao>(_ => new UserDao(connectionString));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IMedicationService, MedicationService>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

// Add framework services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MedReminder API", Version = "v1" });
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }
});

var app = builder.Build();

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.MapControllers();
app.UseAuthorization();

app.Run();
