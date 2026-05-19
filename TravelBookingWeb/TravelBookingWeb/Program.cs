using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using TravelBookingWeb.Data;
using TravelBookingWeb.Models;
using TravelBookingWeb.Repositories;
using TravelBookingWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Configurare Servicii de bază
builder.Services.AddControllersWithViews();

// 2. Configurare Bază de Date (SQL Server)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TravelBookingDB")));

// 3. Înregistrarea Repository-urilor (Stratul de Acces Date)
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IZborRepository, ZborRepository>();
builder.Services.AddScoped<IActivitateRepository, ActivitateRepository>();
builder.Services.AddScoped<IInchirieriRepository, InchirieriRepository>();
builder.Services.AddScoped<IDestinatieRepository, DestinatieRepository>();
builder.Services.AddScoped<IRezervareRepository, RezervareRepository>();

// 4. Înregistrarea Serviciilor (Stratul de Logică Business)
builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IZborService, ZborService>();
builder.Services.AddScoped<IActivitateService, ActivitateService>();
builder.Services.AddScoped<IInchirieriService, InchirieriService>();
builder.Services.AddScoped<IDestinatieService, DestinatieService>();
builder.Services.AddScoped<IRezervareService, RezervareService>();
builder.Services.AddScoped<IAuthService, AuthService>();

// 5. Configurare Autentificare cu ASP.NET Core Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole<int>>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// 5.1 INJECTĂM CLASA NOUĂ PENTRU COOKIE-URI (Decuplarea bazei de date de View)
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, CustomUserClaimsPrincipalFactory>();

// 5.2 Configurare Cookie-uri specifice pentru Identity
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Home/Info";
});

var app = builder.Build();

// 6. Configurare Pipeline HTTP (Middleware)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

// 7. Seeding Bază de Date
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<int>>>();

        DbSeeder.SeedAsync(context, userManager, roleManager).Wait();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Eroare critică la Seeding: " + ex.Message);
    }
}

app.Run();