using Microsoft.EntityFrameworkCore;
using ToDoAndBeyond.Database;
using ToDoAndBeyond.Interfaces;
using ToDoAndBeyond.Models;

namespace ToDoAndBeyond.Classes.ToDoInitialProjects;

public class ToDoCompletedProject(ApplicationDBContext dbContext) : ITodoInitialProject
{
    public ToDoProject InitialProject =>
        new()
        {
            Name = "~Completed",
            Icon = "✅",
            Text = "Contains all completed tasks.",
        };

    public async Task<IEnumerable<ToDoTask>> GetToDoTasks() =>
        await dbContext.Tasks.Where(task => task.IsCompleted).ToListAsync();
}
