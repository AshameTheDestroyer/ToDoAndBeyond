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
            : dbContext.Projects.Add(new ToDoProject() { Name = "First Project" }).Entity;
        hasPopulated |= dbContext.SaveChanges() > 0;

        hasPopulated |= dbContext.EnsurePopulatedWithInitialProjects();

        ToDoTask task = dbContext.Tasks.Any()
            ? dbContext.Tasks.OrderBy(task => task.ID).First()
            : dbContext
                .Tasks.Add(new ToDoTask() { ProjectID = project.ID, Name = "First Task" })
                .Entity;
        hasPopulated |= dbContext.SaveChanges() > 0;

        hasPopulated |= dbContext.EnsurePopulatedWithInitialTasks(project.ID);

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
        dbContext.Projects.AddRange(
            [
                new ToDoMyDayProject(dbContext).InitialProject,
                new ToDoAllProject(dbContext).InitialProject,
                new ToDoCompletedProject(dbContext).InitialProject,
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
                new ToDoTask() { Name = "Go to School", ProjectID = projectID },
                new ToDoTask()
                {
                    Name = "Study for College",
                    ProjectID = projectID,
                    Importance = ToDoTaskImportance.Serious,
                },
                new ToDoTask()
                {
                    Name = "Find a Job",
                    ProjectID = projectID,
                    Importance = ToDoTaskImportance.Mandatory,
                },
                new ToDoTask()
                {
                    Name = "Get a GF",
                    ProjectID = projectID,
                    Importance = ToDoTaskImportance.Deadly,
                },
                new ToDoTask()
                {
                    Name = "Uncheck This",
                    ProjectID = projectID,
                    IsCompleted = true,
                },
                new ToDoTask()
                {
                    Name = "My Favourite Child",
                    ProjectID = projectID,
                    IsStarred = true,
                },
                new ToDoTask()
                {
                    Name = "To Be Done",
                    ProjectID = projectID,
                    DueTime = DateTime.Now,
                },
            ]
        );

        return dbContext.SaveChanges() > 0;
    }
}
