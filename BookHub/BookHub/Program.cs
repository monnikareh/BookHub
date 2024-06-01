using Azure.Identity;
using BusinessLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Middleware;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
var postgresConnectionString = "";

if (builder.Environment.IsEnvironment("ProductionKube"))
{
    postgresConnectionString = configuration.GetConnectionString("KubeConnectionString") ??
                               throw new InvalidOperationException(
                                   "Connection string 'KubeConnectionString' not found.");
}
else if (builder.Environment.IsEnvironment("ProductionAzure"))
{
    Console.Out.WriteLine("Getting access token from Azure AD...");

    // Azure AD resource ID for Azure Database for PostgreSQL Flexible Server is https://server-name.database.windows.net/
    string accessToken = null;

    try
    {
        // Call managed identities for Azure resources endpoint.
        var sqlServerTokenProvider = new DefaultAzureCredential();
        accessToken = (await sqlServerTokenProvider.GetTokenAsync(
            new Azure.Core.TokenRequestContext(scopes: new string[] { "https://hw2-db-pg.postgres.database.azure.com/.default" }) { })).Token;

    }
    catch (Exception e)
    {
        Console.Out.WriteLine("{0} \n\n{1}", e.Message, e.InnerException != null ? e.InnerException.Message : "Acquire token failed");
        System.Environment.Exit(1);
    }
    // postgresConnectionString = Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_AZURE");
    postgresConnectionString = String.Format(
        "Server={0}; User Id={1}; Database={2}; Port={3}; Password={4}; SSLMode=Prefer",
        "hw2-db-pg.postgres.database.azure.com",
        "postgres",
        "bookhub",
        5432,
        accessToken);
}
else
{
    postgresConnectionString = configuration.GetConnectionString("PostgresConnectionString") ??
                               throw new InvalidOperationException(
                                   "Connection string 'PostgresConnectionString' not found.");
}

Console.WriteLine($"Connection String: {postgresConnectionString}");

builder.Services.AddDbContext<BookHubDbContext>(options =>
    options.UseNpgsql(postgresConnectionString,
        x => x.MigrationsAssembly("DAL.Postgres.Migrations")));

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
    .AddCookie();

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
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseMiddleware<RequestLoggerMiddleware>("BookHubWeb");

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
