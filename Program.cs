using Microsoft.EntityFrameworkCore;
using ToDoAndBeyond.Database;
using ToDoAndBeyond.Interfaces;
using ToDoAndBeyond.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ToDoAndBeyondConnection"])
);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddInteractiveServerComponents();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder
    .Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddScoped<IToDoStepRepository, ToDoStepRepository>();
builder.Services.AddScoped<IToDoTaskRepository, ToDoTaskRepository>();
builder.Services.AddScoped<IToDoProjectRepository, ToDoProjectRepository>();

var app = builder.Build();

app.UseRouting();
app.MapBlazorHub();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

using var scope = app.Services.CreateScope();
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
        case "resetting":
            context.EnsureEmpty();
            context.EnsurePopulated();
            break;
    }
}

app.Run();
