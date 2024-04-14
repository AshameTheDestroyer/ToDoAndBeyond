namespace ToDoAndBeyond.DTOs;

public record ProjectsDTO
{
    public required IEnumerable<IEnumerable<ToDoProjectDTO>> ProjectChunks { get; set; }
    public IEnumerable<ToDoTaskDTO>? Tasks { get; set; }
}
