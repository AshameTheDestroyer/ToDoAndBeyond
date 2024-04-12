using Microsoft.EntityFrameworkCore;
// using ToDoAndBeyond.Interfaces;
using ToDoAndBeyond.Models;

// using ToDoAndBeyond.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ToDoAndBeyondConnection"])
);

builder.Services.AddControllersWithViews();

// builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationDBContext>();
    context.Database.EnsureCreated();
}

app.Run();
