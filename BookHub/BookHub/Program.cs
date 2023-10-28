using System.Reflection;
using System.Text;
using BusinessLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


var configuration = builder.Configuration;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var connectionString = configuration.GetConnectionString("ConnectionString") ??
                       throw new InvalidOperationException("Connection string 'ConnectionString' not found.");

builder.Services.AddLogging();
builder.Services.AddDbContext<BookHubDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddIdentity<User, IdentityRole<int>>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BookHubDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = configuration["JWT:ValidAudience"],
            ValidIssuer = configuration["JWT:ValidIssuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
        };
    });

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    ).AddApplicationPart(Assembly.Load("WebAPI"));
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "BookHub API", 
        Version = "v1" 
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
        In = ParameterLocation.Header, 
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"

    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { 
            new OpenApiSecurityScheme 
            { 
                Reference = new OpenApiReference 
                { 
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer" 
                } 
            },
            Array.Empty<string>()
        } 
    });
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IAuthorService, AuthorService>();

var app = builder.Build();


// WE WANT SWAGGER IN PRODUCTION AS WELL
app.UseMigrationsEndPoint();
app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<RequestLoggerMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();
app.UseCors();
app.Run();