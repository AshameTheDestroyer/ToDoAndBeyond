using ToDoAndBeyond.Models;

namespace ToDoAndBeyond.Interfaces;

public interface IToDoTaskRepository
{
    public Task<IEnumerable<ToDoTask>> GetToDoTasks();
    public bool AddToDoTask(ToDoTask task);
    public bool DeleteToDoTask(ToDoTask task);
    public bool UpdateToDoTask(ToDoTask task);
    public bool Save();
}
