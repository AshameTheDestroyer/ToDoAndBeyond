using Microsoft.EntityFrameworkCore;
using ToDoAndBeyond.Database;
using ToDoAndBeyond.Interfaces;
using ToDoAndBeyond.Models;

namespace ToDoAndBeyond.Classes.ToDoInitialProjects;

public class ToDoUncompletedProject(ApplicationDBContext dbContext) : ITodoInitialProject
{
    public ToDoProject InitialProject =>
        new()
        {
            Name = "~Uncompleted",
            Icon = "🟩",
            Text = "Contains all uncompleted tasks.",
        };

    public async Task<IEnumerable<ToDoTask>> GetToDoTasks() =>
        await dbContext.Tasks.Where(task => !task.IsCompleted).ToListAsync();
}
