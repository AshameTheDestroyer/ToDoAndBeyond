using Microsoft.EntityFrameworkCore;
using ToDoAndBeyond.Interfaces;
using ToDoAndBeyond.Models;

namespace ToDoAndBeyond.Services;

public class ToDoTaskRepository(ApplicationDBContext dbContext) : IToDoTaskRepository
{
    private readonly ApplicationDBContext dbContext = dbContext;

    public async Task<IEnumerable<ToDoTask>> GetToDoTasks() => await dbContext.Tasks.ToListAsync();

    public bool AddToDoTask(ToDoTask task)
    {
        dbContext.Tasks.Add(task);
        return Save();
    }

    public bool DeleteToDoTask(ToDoTask task)
    {
        dbContext.Tasks.Remove(task);
        return Save();
    }

    public bool UpdateToDoTask(ToDoTask task)
    {
        dbContext.Tasks.Update(task);
        return Save();
    }

    public bool Save() => dbContext.SaveChanges() > 0;
}
