using VBookHaven.Models;
using Microsoft.EntityFrameworkCore;
using VBookHaven.Respository;
using VBookHaven.Services;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
var service = builder.Services;
service.AddControllersWithViews();
service.AddSession(opt => opt.IdleTimeout = TimeSpan.FromMinutes(60));
service.AddDbContext<VBookHavenDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
service.AddTransient<IAccountRespository, AccountRespository>();
service.AddTransient<IEmailSender, EmailSender>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Customer}/{controller=Account}/{action=Login}/{id?}");

app.Run();
