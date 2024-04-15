using Microsoft.EntityFrameworkCore;
using ToDoAndBeyond.Database;
using ToDoAndBeyond.Interfaces;
using ToDoAndBeyond.Models;

namespace ToDoAndBeyond.Repositories;

public class ToDoProjectRepository(ApplicationDBContext dbContext) : IToDoProjectRepository
{
    private readonly ApplicationDBContext dbContext = dbContext;

    public async Task<IEnumerable<ToDoProject>> GetToDoProjects() =>
        await dbContext.Projects.ToListAsync();

    public async Task<ToDoProject?> GetToDoProject(int id) =>
        await dbContext.Projects.FirstAsync(project => project.ID == id);

    public bool AddToDoProject(ToDoProject project)
    {
        dbContext.Projects.Add(project);
        return Save();
    }

    public bool DeleteToDoProject(ToDoProject project)
    {
        dbContext.Projects.Remove(project);
        return Save();
    }

    public bool UpdateToDoProject(ToDoProject project)
    {
        dbContext.Projects.Update(project);
        return Save();
    }

    public bool Save() => dbContext.SaveChanges() > 0;
}
