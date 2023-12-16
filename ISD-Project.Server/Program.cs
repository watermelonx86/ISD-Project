using ISD_Project.Server;
using ISD_Project.Server.DataAccess;
using ISD_Project.Server.Profiles;
using ISD_Project.Server.Services;
using ISD_Project.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// CORS: https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-8.0
builder.Services.AddCors(options =>
{
    options.AddPolicy(MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:5173", "http://localhost:5174")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Services for Authentication and Authorization with JWT
string tokenValue = builder.Configuration.GetSection("AppSettings:Token").Value ?? String.Empty;
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenValue)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
// Add Services AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));


// inject db context
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
// inject services
builder.Services.AddScoped<ICryptoService, CryptoService>();
builder.Services.AddScoped<IUserAccountService, UserAccountService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IHealthInformationService, HealthInformationService>();
builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IInsuranceService, InsuranceService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IApprovalStatusService, ApprovalStatusService>();
builder.Services.AddScoped<IInsuranceContractService, InsuranceContractService>();
//
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins); // use CORS

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
