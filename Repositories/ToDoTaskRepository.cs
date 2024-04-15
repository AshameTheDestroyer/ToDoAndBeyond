using Microsoft.EntityFrameworkCore;
using ToDoAndBeyond.Classes.ToDoInitialProjects;
using ToDoAndBeyond.Database;
using ToDoAndBeyond.Enums;
using ToDoAndBeyond.Interfaces;
using ToDoAndBeyond.Models;

namespace ToDoAndBeyond.Repositories;

public class ToDoTaskRepository(ApplicationDBContext dbContext) : IToDoTaskRepository
{
    private readonly ApplicationDBContext dbContext = dbContext;
    private readonly string ToDoInitialProjectNamespace =
        $"{nameof(ToDoAndBeyond)}.{nameof(Classes)}.{nameof(Classes.ToDoInitialProjects)}";

    public async Task<IEnumerable<ToDoTask>> GetToDoTasks() => await dbContext.Tasks.ToListAsync();

    public async Task<IEnumerable<ToDoTask>> GetToDoTasks(int projectID) =>
        await dbContext.Tasks.Where(task => task.ProjectID == projectID).ToListAsync();

    public async Task<IEnumerable<ToDoTask>> GetToDoTasks(int projectID, string projectName)
    {
        if (!projectName.StartsWith("~"))
        {
            return await GetToDoTasks(projectID);
        }

        var name = projectName.Replace("~", "");
        var initialProjectType = Type.GetType($"{ToDoInitialProjectNamespace}.ToDo{name}Project");
        bool isImportanceProject = Enum.GetNames<ToDoTaskImportance>().Contains(name);

        if (isImportanceProject)
        {
            initialProjectType = typeof(ToDoImportanceProjects);
        }

        object? instance = isImportanceProject
            ? new ToDoImportanceProjects(dbContext, Enum.Parse<ToDoTaskImportance>(name))
            : initialProjectType != null
                ? Activator.CreateInstance(initialProjectType, dbContext)
                : null;

        if (instance == null)
        {
            return [];
        }

        return (
                initialProjectType
                    ?.GetMethod(nameof(ITodoInitialProject.GetToDoTasks))
                    ?.Invoke(instance, []) as Task<IEnumerable<ToDoTask>>
            )?.Result ?? [new ToDoTask() { Name = "aaaaaa", ProjectID = 3 }];
    }

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
