using VBookHaven.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using VBookHaven.Respository;
using Microsoft.AspNetCore.Identity.UI.Services;
using VBookHaven.Utility;
using Microsoft.AspNetCore.Authentication.Cookies;
using VBookHaven.DbInitializer;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var service = builder.Services;

service.AddControllersWithViews();
service.AddDbContext<VBookHavenDBContext>(options =>
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
service.AddSession(opt => opt.IdleTimeout = TimeSpan.FromMinutes(60));
builder.Services.Configure<CookieAuthenticationOptions>(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromMinutes(120);
});


service.AddScoped<IProductRespository, ProductRespository>();
service.AddScoped<IAuthorRepository, AuthorRepository>();
service.AddScoped<ICategoryRepository, CategoryRepository>();
service.AddScoped<IImageRepository, ImageRepository>();

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
    pattern: "{area=Customer}/{controller=Product}/{action=Index}/{id?}");

app.Run();

void SeedDatabase()
{
    using (var scope = app.Services.CreateScope())
    {
        var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
        dbInitializer.Initialize();
    }
}