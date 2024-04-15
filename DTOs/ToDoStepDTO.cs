namespace ToDoAndBeyond.DTOs;

public record ToDoStepDTO
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public bool IsCompleted { get; set; } = false;
}
