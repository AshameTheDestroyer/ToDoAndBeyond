namespace ToDoAndBeyond.DTOs;

public record ProjectsDTO
{
    public ToDoTaskDTO? SelectedTask { get; set; }
    public ToDoProjectDTO? SelectedProject { get; set; }
    public required IEnumerable<IEnumerable<ToDoProjectDTO>> ProjectChunks { get; set; }
    public IEnumerable<ToDoTaskDTO>? Tasks { get; set; }
    public IEnumerable<ToDoStepDTO>? Steps { get; set; }
    public string? SearchTerm { get; set; }
}

public record NestableToDoTaskDTO(ToDoTaskDTO Task, int NestingLevel, int GapLevel);

public static class ProjectsDTORenderingExtensions
{
    public static IEnumerable<NestableToDoTaskDTO>? GetNestableToDoTasks(
        this ProjectsDTO projectsDTO
    )
    {
        if (projectsDTO.Tasks == null)
        {
            return null;
        }

        var rootTasks = projectsDTO
            .Tasks.Where(task => task.ParentTaskID == null)
            .Select(task => new NestableToDoTaskDTO(task, NestingLevel: 0, GapLevel: 0))
            .ToList();

        var nestedTasks = projectsDTO.Tasks.Where(task => task.ParentTaskID != null).ToList();
        foreach (var nestedTask in nestedTasks)
        {
            var parentTask = rootTasks.FirstOrDefault(
                nestableTask => nestableTask?.Task.ID == nestedTask.ParentTaskID,
                null
            );

            if (parentTask == null)
            {
                rootTasks.Add(
                    new NestableToDoTaskDTO(Task: nestedTask, NestingLevel: 0, GapLevel: 0)
                );
                continue;
            }

            rootTasks.Insert(
                rootTasks.IndexOf(parentTask) + 1,
                new NestableToDoTaskDTO(
                    Task: nestedTask,
                    NestingLevel: parentTask.NestingLevel + 1,
                    GapLevel: 1
                )
            );
        }

        for (int i = rootTasks.Count - 1; i >= 1; i--)
        {
            if (rootTasks[i].NestingLevel == 0)
            {
                continue;
            }

            for (int j = i - 1; j >= 0; j--)
            {
                if (rootTasks[i].NestingLevel > rootTasks[j].NestingLevel)
                {
                    break;
                }
                rootTasks[i] = rootTasks[i] with { GapLevel = rootTasks[i].GapLevel + 1 };
            }
        }

        return rootTasks;
    }
}
