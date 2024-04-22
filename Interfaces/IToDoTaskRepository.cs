using ToDoAndBeyond.Models;

namespace ToDoAndBeyond.Interfaces;

public interface IToDoTaskRepository
{
    public Task<IEnumerable<ToDoTask>> GetToDoTasks();
    public Task<ToDoTask?> GetToDoTask(int id);
    public Task<IEnumerable<ToDoTask>> GetToDoTasks(int projectID);
    public Task<IEnumerable<ToDoTask>> GetToDoTasks(int projectID, string projectName);
    public bool UpdateIsCompleted(int id, bool value);
    public bool AddToDoTask(ToDoTask task);
    public bool DeleteToDoTask(ToDoTask task);
    public bool UpdateToDoTask(ToDoTask task);
    public bool Save();
}
