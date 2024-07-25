using EccomerceWebsiteProject.Core;
using EccomerceWebsiteProject.Core.Models.PlatformUsers;
using EccomerceWebsiteProject.Infrastructure.DatabaseContext;
using EccomerceWebsiteProject.Infrastructure.Services.IserviceCoreInterface.IProductServices;
using EccomerceWebsiteProject.Infrastructure.Services.ProductServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<EccomerceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DevConnections"));
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc(
        "v1",
        new OpenApiInfo { Title = "EccomerceWebsiteProject.API", Version = "v1" }
    );

    // Add security definition for Bearer token
    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please Insert token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer"
        }
    );

    // Add security requirement for Bearer token
    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                   }
                },
                new string[] { }
            }
        }
    );
});



builder.Services.AddIdentity<CreateAllPlatformUserModel, IdentityRole>()
    .AddEntityFrameworkStores<EccomerceDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<UserManager<CreateAllPlatformUserModel>>();
builder.Services.AddScoped<IproductService, ProductServices>();
builder.Services.AddSingleton<SecretKeyGenerator>();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8; // Minimum password length
    options.Password.RequiredUniqueChars = 6; // Minimum unique characters in password
});


// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Jwt:SecretKey"])),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddHttpClient("mpesa", c =>
{
    c.BaseAddress = new Uri("https://sandbox.safaricom.co.ke");
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

// Secret key generation method
string GenerateSecretKey()
{
    var keyGenerator = new SecretKeyGenerator();
    return keyGenerator.GenerateSecretKey();
}

