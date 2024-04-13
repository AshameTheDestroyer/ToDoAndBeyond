using ToDoAndBeyond.Enums;
using ToDoAndBeyond.Models;

namespace ToDoAndBeyond.Database;

public static class ApplicationDBContextExtensions
{
    public static bool EnsurePopulated(this ApplicationDBContext dbContext)
    {
        bool hasPopulated = false;

        ToDoProject? project = dbContext.Projects.Any()
            ? dbContext.Projects.OrderBy(project => project.ID).First()
            : dbContext.Projects.Add(new ToDoProject() { Name = "First Project" }).Entity;
        hasPopulated |= dbContext.SaveChanges() > 0;

        hasPopulated |= dbContext.EnsurePopulatedWithInitialProjects();

        ToDoTask task = dbContext.Tasks.Any()
            ? dbContext.Tasks.OrderBy(task => task.ID).First()
            : dbContext
                .Tasks.Add(new ToDoTask() { ProjectID = project.ID, Name = "First Task" })
                .Entity;
        hasPopulated |= dbContext.SaveChanges() > 0;

        ToDoStep _step = dbContext.Steps.Any()
            ? dbContext.Steps.OrderBy(step => step.ID).First()
            : dbContext.Steps.Add(new ToDoStep() { TaskID = task.ID, Name = "First Step" }).Entity;
        return hasPopulated |= dbContext.SaveChanges() > 0;
    }

    public static bool EnsureEmpty(this ApplicationDBContext dbContext)
    {
        dbContext.Steps.RemoveRange(dbContext.Steps.ToList());
        dbContext.Tasks.RemoveRange(dbContext.Tasks.ToList());
        dbContext.Projects.RemoveRange(dbContext.Projects.ToList());

        return dbContext.SaveChanges() > 0;
    }

    private static bool EnsurePopulatedWithInitialProjects(this ApplicationDBContext dbContext)
    {
        var importanceProjects = Enum.GetNames<ToDoTaskImportance>()
            .Select(
                (name, i) =>
                    new ToDoProject()
                    {
                        Name = $"~{name}",
                        Icon = new string[] { "📃", "🥉", "🥈", "🥇" }[i],
                        Text = $"Contains all tasks with the importance level of {name}.",
                    }
            );

        dbContext.Projects.AddRange(
            [
                new ToDoProject()
                {
                    Name = "~My Day",
                    Icon = "☀️",
                    Text = "Contains all tasks due today.",
                },
                new ToDoProject()
                {
                    Name = "~All",
                    Icon = "♾️",
                    Text = "Contains all tasks.",
                },
                new ToDoProject()
                {
                    Name = "~Completed",
                    Icon = "✅",
                    Text = "Contains all completed tasks.",
                },
                new ToDoProject()
                {
                    Name = "~Starred",
                    Icon = "⭐",
                    Text = "Contains all starred tasks.",
                },
                .. importanceProjects,
            ]
        );

        return dbContext.SaveChanges() > 0;
    }
}
