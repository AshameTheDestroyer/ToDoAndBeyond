using ToDoAndBeyond.Models;

namespace ToDoAndBeyond.Interfaces;

public interface IToDoStepRepository
{
    public Task<IEnumerable<ToDoStep>> GetToDoSteps();
    public bool AddToDoStep(ToDoStep step);
    public bool DeleteToDoStep(ToDoStep step);
    public bool UpdateToDoStep(ToDoStep step);
    public bool Save();
}
