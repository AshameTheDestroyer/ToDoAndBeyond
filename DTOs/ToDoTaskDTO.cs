using ToDoAndBeyond.Enums;

namespace ToDoAndBeyond.DTOs;

public record ToDoTaskDTO
{
    public int ID { get; set; }
    public int? ParentTaskID { get; set; }
    public required string Name { get; set; }
    public string? Icon { get; set; }
    public DateTime? DueTime { get; set; }
    public bool IsCompleted { get; set; } = false;
    public bool IsStarred { get; set; } = false;
    public string? Colour { get; set; }
    public ToDoTaskImportance Importance { get; set; } = ToDoTaskImportance.Regular;
    public string? ImportanceIcon { get; set; }
    public string? ImportanceColour { get; set; }
}
