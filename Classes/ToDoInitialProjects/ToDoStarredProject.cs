using Microsoft.EntityFrameworkCore;
using ToDoAndBeyond.Database;
using ToDoAndBeyond.Interfaces;
using ToDoAndBeyond.Models;

namespace ToDoAndBeyond.Classes.ToDoInitialProjects;

public class ToDoStarredProject(ApplicationDBContext dbContext) : ITodoInitialProject
{
    public ToDoProject InitialProject =>
        new()
        {
            Name = "~Starred",
            Icon = "⭐",
            Text = "Contains all starred tasks.",
        };

    public async Task<IEnumerable<ToDoTask>> GetToDoTasks() =>
        await dbContext.Tasks.Where(task => task.IsStarred).ToListAsync();
}
