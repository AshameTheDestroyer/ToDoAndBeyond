using Microsoft.EntityFrameworkCore;

namespace ToDoAndBeyond.Models;

public class ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
    : DbContext(options)
{
    // public DbSet<User> Users { get; private set; }
}
