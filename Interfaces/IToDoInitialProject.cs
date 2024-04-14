using ToDoAndBeyond.Database;
using ToDoAndBeyond.Models;

namespace ToDoAndBeyond.Interfaces;

public interface ITodoInitialProject
{
    public ToDoProject InitialProject { get; }
    public Task<IEnumerable<ToDoTask>> GetToDoTasks();
}
