namespace ToDoAndBeyond.DTOs;

public record ToDoProjectDTO
{
    public int ID { get; set; }
    public required string Name { get; set; }
    public string? Icon { get; set; }
}
