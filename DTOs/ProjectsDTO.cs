namespace ToDoAndBeyond.DTOs;

public record ProjectsDTO
{
    public ToDoProjectDTO? SelectedProject { get; set; }
    public required IEnumerable<IEnumerable<ToDoProjectDTO>> ProjectChunks { get; set; }
    public IEnumerable<ToDoTaskDTO>? Tasks { get; set; }
    public IEnumerable<ToDoStepDTO>? Steps { get; set; }
}
