using Microsoft.EntityFrameworkCore;
using ToDoAndBeyond.Database;
using ToDoAndBeyond.Interfaces;
using ToDoAndBeyond.Models;

namespace ToDoAndBeyond.Classes.ToDoInitialProjects;

public class ToDoMyDayProject(ApplicationDBContext dbContext) : ITodoInitialProject
{
    public ToDoProject InitialProject =>
        new()
        {
            Name = "~My Day",
            Icon = "☀️",
            Text = "Contains all tasks due today.",
        };

    public async Task<IEnumerable<ToDoTask>> GetToDoTasks() =>
        await dbContext
            .Tasks.Where(task => task.DueTime != null && task.DueTime.Value.Day == DateTime.Now.Day)
            .ToListAsync();
}
