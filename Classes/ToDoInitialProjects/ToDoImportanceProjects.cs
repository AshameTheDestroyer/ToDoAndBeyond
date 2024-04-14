using Microsoft.EntityFrameworkCore;
using ToDoAndBeyond.Database;
using ToDoAndBeyond.Enums;
using ToDoAndBeyond.Interfaces;
using ToDoAndBeyond.Models;

namespace ToDoAndBeyond.Classes.ToDoInitialProjects;

public class ToDoImportanceProjects(
    ApplicationDBContext dbContext,
    ToDoTaskImportance taskImportance
) : ITodoInitialProject
{
    private static readonly string[] initialProjectIcons = ["📃", "🥉", "🥈", "🥇"];

    public ToDoTaskImportance TaskImportance { get; } = taskImportance;
    public ToDoProject InitialProject =>
        InitialProjects.First(initialProject =>
            initialProject.Name.Contains(TaskImportance.ToString())
        );

    public static IEnumerable<ToDoProject> InitialProjects =>
        Enum.GetNames<ToDoTaskImportance>()
            .Select(
                (name, i) =>
                    new ToDoProject()
                    {
                        Name = $"~{name}",
                        Icon = initialProjectIcons[i],
                        Text = $"Contains all tasks with the importance level of {name}.",
                    }
            );

    public async Task<IEnumerable<ToDoTask>> GetToDoTasks() =>
        await dbContext.Tasks.Where(task => task.Importance == TaskImportance).ToListAsync();
}
