using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mtg.Data;
using mtg.Library.Seeders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => 
            {
            //options.SignIn.RequireConfirmedAccount = true
            // Require settings
            options.Password.RequireDigit = true; 
            options.Password.RequiredLength = 8; 
            options.Password.RequireNonAlphanumeric = false; 
            options.Password.RequireUppercase = true; 
            options.Password.RequireLowercase = false; 
            options.Password.RequiredUniqueChars = 6;
            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30); 
            options.Lockout.MaxFailedAccessAttempts = 10; 
            options.Lockout.AllowedForNewUsers = true;
            // User settings
            options.User.RequireUniqueEmail = true;
            }
        )
    .AddRoles<IdentityRole>() // ADD ROLES
    .AddEntityFrameworkStores<ApplicationDbContext>();
    builder.Services.AddControllersWithViews();
    builder.Services.AddRazorPages();
    builder.Services.AddSession();

IServiceProvider serviceProvider = builder.Services.BuildServiceProvider();
var authSeeder = new AuthSeeder(serviceProvider);
authSeeder.Run();

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession(new SessionOptions
{
    Cookie = new CookieBuilder
    {
        Name = ".EE.Session",
        IsEssential = true
    }
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

