using Microsoft.EntityFrameworkCore;
using ToDoAndBeyond.Database;
using ToDoAndBeyond.Interfaces;
using ToDoAndBeyond.Models;

namespace ToDoAndBeyond.Classes.ToDoInitialProjects;

public class ToDoAllProject(ApplicationDBContext dbContext) : ITodoInitialProject
{
    public ToDoProject InitialProject =>
        new()
        {
            Name = "~All",
            Icon = "♾️",
            Text = "Contains all tasks.",
        };

    public async Task<IEnumerable<ToDoTask>> GetToDoTasks() => await dbContext.Tasks.ToListAsync();
}
