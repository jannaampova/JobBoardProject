using JobBoard.Data; 
using JobBoard.Services;
using Microsoft.EntityFrameworkCore;
using JobBoard.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;


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
    .AddEntityFrameworkStores<ApplicationDbContext>()
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
    options.AddPolicy("Candidate", policy => policy.RequireRole("Candidate"));

});

// Register AccountService
builder.Services.AddScoped<IAccountService, AccountService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
