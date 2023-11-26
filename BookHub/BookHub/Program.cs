using System.Text;
using BusinessLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Middleware;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var postgresConnectionString = configuration.GetConnectionString("PostgresConnectionString") ??
                               throw new InvalidOperationException(
                                   "Connection string 'PostgresConnectionString' not found.");

var mariadbConnectionString = configuration.GetConnectionString("MariaDBConnectionString") ??
                              throw new InvalidOperationException(
                                  "Connection string 'MariaDBConnectionString' not found.");

builder.Services.AddDbContext<BookHubDbContext>(options =>
    options.UseNpgsql(postgresConnectionString,
        x => x.MigrationsAssembly("DAL.Postgres.Migrations")));

// builder.Services.AddDbContext<BookHubDbContext>(
//     options => options
//         .UseMySql(mariadbConnectionString, ServerVersion.Create(new Version(10, 5, 4), ServerType.MariaDb),
//             x => x.MigrationsAssembly("DAL.MariaDB.Migrations")));

builder.Services.AddLogging();

builder.Services.AddIdentity<User, IdentityRole<int>>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.User.RequireUniqueEmail = true;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddEntityFrameworkStores<BookHubDbContext>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();
builder.Services.AddControllersWithViews();
builder.Services
    .AddAuthentication()
    .AddCookie()
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["JWT:ValidIssuer"],
            ValidAudience = configuration["JWT:ValidAudience"],
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"] ?? string.Empty))
        };
    });

builder.Services.AddRazorPages();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddTransient<IBookService, BookService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IAuthorService, AuthorService>();
builder.Services.AddTransient<IGenreService, GenreService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRatingService, RatingService>();
builder.Services.AddTransient<IPublisherService, PublisherService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


// WE WANT SWAGGER IN PRODUCTION AS WELL
app.UseMiddleware<RequestLoggerMiddleware>();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.UseCors();
app.Run();