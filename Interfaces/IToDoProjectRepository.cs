using ToDoAndBeyond.Models;

namespace ToDoAndBeyond.Interfaces;

public interface IToDoProjectRepository
{
    public Task<IEnumerable<ToDoProject>> GetToDoProjects();
    public Task<ToDoProject?> GetToDoProject(int id);
    public bool AddToDoProject(ToDoProject project);
    public bool DeleteToDoProject(ToDoProject project);
    public bool UpdateToDoProject(ToDoProject project);
    public bool Save();
}
