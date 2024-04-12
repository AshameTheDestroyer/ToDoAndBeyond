using Microsoft.EntityFrameworkCore;
using ToDoAndBeyond.Database;
using ToDoAndBeyond.Interfaces;
using ToDoAndBeyond.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ToDoAndBeyondConnection"])
);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IToDoProjectRepository, ToDoProjectRepository>();
builder.Services.AddScoped<IToDoTaskRepository, ToDoTaskRepository>();
builder.Services.AddScoped<IToDoStepRepository, ToDoStepRepository>();

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

    if (args.Length > 0)
    {
        switch (args[0])
        {
            case "seeding":
                context.EnsurePopulated();
                break;
            case "emptying":
                context.EnsureEmpty();
                break;
        }
    }
}

app.Run();
