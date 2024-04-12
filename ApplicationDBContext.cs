using Microsoft.EntityFrameworkCore;

namespace ToDoAndBeyond.Models;

public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
    : DbContext(options)
{
    public DbSet<ToDoProject> Projects { get; private set; }
    public DbSet<ToDoTask> Tasks { get; private set; }
    public DbSet<ToDoStep> Steps { get; private set; }
}
