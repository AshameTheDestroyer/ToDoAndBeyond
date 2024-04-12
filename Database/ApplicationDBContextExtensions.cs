using ToDoAndBeyond.Models;

namespace ToDoAndBeyond.Database;

public static class ApplicationDBContextExtensions
{
    public static bool EnsurePopulated(this ApplicationDBContext dbContext)
    {
        bool hasPopulated = false;

        ToDoProject? project = dbContext.Projects.Any()
            ? dbContext.Projects.OrderBy(project => project.ID).First()
            : dbContext
                .Projects.Add(
                    new ToDoProject()
                    {
                        // UserID = 1,
                        Name = "First Project",
                    }
                )
                .Entity;
        hasPopulated |= dbContext.SaveChanges() > 0;

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
}
