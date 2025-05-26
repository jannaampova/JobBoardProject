using JobBoard.Data; 
using JobBoard.Services;
using Microsoft.EntityFrameworkCore;
using JobBoard.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using JobBoard.Models.plainModels;
using System.Reflection.Emit;
using JobBoard.Services.Company;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Настройка на Identity

builder.Services.AddIdentity<UserData, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationIdentityDbContext>()
    .AddDefaultTokenProviders();

// Добавяне на аутентикация
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/access-denied";
        options.LogoutPath = "/logout";
    });

// Добавяне на авторизация
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Company", policy => policy.RequireRole("Company"));
    options.AddPolicy("Company", policy => policy.RequireRole("Company"));
    options.AddPolicy("Candidate", policy => policy.RequireRole("Candidate"));

});

// Register
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped< FilterService>();
builder.Services.AddScoped< JobDetailsService>(); 
builder.Services.AddScoped< SavedListingService>(); 
builder.Services.AddScoped< AccountSettingsService>(); 
builder.Services.AddScoped< ApplyService>(); 
builder.Services.AddScoped< ApplicationsService>(); 
builder.Services.AddScoped< CompanyApplicationsService>(); 

builder.Services.AddScoped< CreateListingService>(); 
builder.Services.AddScoped< CompanySettingsService>(); 
builder.Services.AddScoped< PeopleService>(); 




var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
