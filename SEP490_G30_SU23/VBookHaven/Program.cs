using VBookHaven.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using VBookHaven.Respository;
using Microsoft.AspNetCore.Identity.UI.Services;
using VBookHaven.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using VBookHaven.DbInitializer;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<VBookHavenDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()//options => options.SignIn.RequireConfirmedAccount = true
    .AddEntityFrameworkStores<VBookHavenDBContext>().AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options => {
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});// add sau identity
builder.Services.AddScoped<IDbInitializer, DbInitializer>();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddSession(opt => opt.IdleTimeout = TimeSpan.FromMinutes(60));
builder.Services.Configure<CookieAuthenticationOptions>(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
});

builder.Services.AddScoped<IApplicationUserRespository, ApplicationUserRespository>();
builder.Services.AddScoped<IProductRespository, ProductRespository>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
//ignore circle
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseSession();
SeedDatabase();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Admin}/{controller=Product}/{action=Index}/{id?}");

app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}