using Microsoft.EntityFrameworkCore;
using ToDoAndBeyond.Interfaces;
using ToDoAndBeyond.Models;

namespace ToDoAndBeyond.Services;

public class ToDoStepRepository(ApplicationDBContext dbContext) : IToDoStepRepository
{
    private readonly ApplicationDBContext dbContext = dbContext;

    public async Task<IEnumerable<ToDoStep>> GetToDoSteps() => await dbContext.Steps.ToListAsync();

    public bool AddToDoStep(ToDoStep step)
    {
        dbContext.Steps.Add(step);
        return Save();
    }

    public bool DeleteToDoStep(ToDoStep step)
    {
        dbContext.Steps.Remove(step);
        return Save();
    }

    public bool UpdateToDoStep(ToDoStep step)
    {
        dbContext.Steps.Update(step);
        return Save();
    }

    public bool Save() => dbContext.SaveChanges() > 0;
}
