using System.Drawing;
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
    public static Dictionary<ToDoTaskImportance, string> TaskImportanceIcons =>
        new()
        {
            [ToDoTaskImportance.Regular] = "📃",
            [ToDoTaskImportance.Serious] = "🥉",
            [ToDoTaskImportance.Mandatory] = "🥈",
            [ToDoTaskImportance.Deadly] = "🥇",
        };

    public static Dictionary<ToDoTaskImportance, string> TaskImportanceColours =>
        new()
        {
            [ToDoTaskImportance.Regular] = "white",
            [ToDoTaskImportance.Serious] = "chocolate",
            [ToDoTaskImportance.Mandatory] = "lightblue",
            [ToDoTaskImportance.Deadly] = "orange",
        };

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
                        Icon = TaskImportanceIcons[Enum.Parse<ToDoTaskImportance>(name)],
                        Text = $"Contains all tasks with the importance level of {name}.",
                    }
            );

    public async Task<IEnumerable<ToDoTask>> GetToDoTasks() =>
        await dbContext.Tasks.Where(task => task.Importance == TaskImportance).ToListAsync();
}
