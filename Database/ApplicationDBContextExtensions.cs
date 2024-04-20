using ToDoAndBeyond.Classes.ToDoInitialProjects;
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
            : dbContext.Projects.Add(new ToDoProject { Name = "First Project" }).Entity;
        hasPopulated |= dbContext.SaveChanges() > 0;

        hasPopulated |= dbContext.EnsurePopulatedWithInitialProjects();

        ToDoTask task = dbContext.Tasks.Any()
            ? dbContext.Tasks.OrderBy(task => task.ID).First()
            : dbContext
                .Tasks.Add(new ToDoTask { ProjectID = project.ID, Name = "First Task" })
                .Entity;
        hasPopulated |= dbContext.SaveChanges() > 0;

        hasPopulated |= dbContext.EnsurePopulatedWithInitialTasks(project.ID);
        hasPopulated |= dbContext.EnsurePopulatedWithNestedTasks(project.ID, nestingCount: 5);

        ToDoStep _step = dbContext.Steps.Any()
            ? dbContext.Steps.OrderBy(step => step.ID).First()
            : dbContext.Steps.Add(new ToDoStep { TaskID = task.ID, Name = "First Step" }).Entity;
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
        dbContext.Projects.AddRange(
            [
                new ToDoMyDayProject(dbContext).InitialProject,
                new ToDoAllProject(dbContext).InitialProject,
                new ToDoCompletedProject(dbContext).InitialProject,
                new ToDoUncompletedProject(dbContext).InitialProject,
                new ToDoStarredProject(dbContext).InitialProject,
                .. ToDoImportanceProjects.InitialProjects,
            ]
        );

        return dbContext.SaveChanges() > 0;
    }

    private static bool EnsurePopulatedWithInitialTasks(
        this ApplicationDBContext dbContext,
        int projectID
    )
    {
        dbContext.Tasks.AddRange(
            [
                new ToDoTask { Name = "Go to School", ProjectID = projectID },
                new ToDoTask
                {
                    Name = "Study for College",
                    ProjectID = projectID,
                    Importance = ToDoTaskImportance.Serious,
                },
                new ToDoTask
                {
                    Name = "Find a Job",
                    ProjectID = projectID,
                    Importance = ToDoTaskImportance.Mandatory,
                },
                new ToDoTask
                {
                    Name = "Get a GF",
                    ProjectID = projectID,
                    Importance = ToDoTaskImportance.Deadly,
                },
                new ToDoTask
                {
                    Name = "Uncheck Me if You Can",
                    ProjectID = projectID,
                    IsCompleted = true,
                },
                new ToDoTask
                {
                    Name = "My Favourite Child",
                    ProjectID = projectID,
                    IsStarred = true,
                },
                new ToDoTask
                {
                    Name = "To Be Done",
                    ProjectID = projectID,
                    DueTime = DateTime.Now,
                },
                new ToDoTask
                {
                    Name = "Never Greenier",
                    ProjectID = projectID,
                    Colour = "#00AF64",
                },
                new ToDoTask
                {
                    Name = "Red as the Sun",
                    ProjectID = projectID,
                    Colour = "red",
                    Importance = ToDoTaskImportance.Deadly,
                },
            ]
        );

        return dbContext.SaveChanges() > 0;
    }

    private static bool EnsurePopulatedWithNestedTasks(
        this ApplicationDBContext dbContext,
        int projectID,
        int nestingCount,
        int? parentTaskID = null
    )
    {
        bool hasPopulated = false;

        ToDoTask? task = null;
        var randomizer = new Random();
        for (int i = 0; i < 2; i++)
        {
            task = dbContext
                .Tasks.Add(
                    new ToDoTask
                    {
                        Name = $"Nested Task{nestingCount}",
                        ProjectID = projectID,
                        ParentTaskID = parentTaskID,
                        Importance = (ToDoTaskImportance)
                            randomizer.Next(Enum.GetValues<ToDoTaskImportance>().Length),
                        IsCompleted = randomizer.Next(2) == 1,
                        IsStarred = randomizer.Next(2) == 1,
                    }
                )
                .Entity;
            hasPopulated |= dbContext.SaveChanges() > 0;
        }

        return nestingCount > 1
            ? hasPopulated |= dbContext.EnsurePopulatedWithNestedTasks(
                projectID,
                nestingCount - 1,
                parentTaskID: task!.ID
            )
            : hasPopulated;
    }
}
