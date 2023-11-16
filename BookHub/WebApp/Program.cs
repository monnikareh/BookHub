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
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


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
app.UseMigrationsEndPoint();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.UseCors();
app.Run();