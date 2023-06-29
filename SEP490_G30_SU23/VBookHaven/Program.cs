using VBookHaven.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using VBookHaven.Respository;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var service = builder.Services;

service.AddControllersWithViews();
service.AddSession(opt => opt.IdleTimeout = TimeSpan.FromMinutes(60));
service.AddDbContext<VBookHavenDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddEntityFrameworkStores<VBookHavenDBContext>();
builder.Services.AddRazorPages();

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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Admin}/{controller=Product}/{action=Index}/{id?}");

app.Run();
