using System.ComponentModel.DataAnnotations;
using ToDoAndBeyond.Enums;

namespace ToDoAndBeyond.Models;

public class ToDoTask
{
    [Key]
    public int ID { get; set; }
    public int ProjectID { get; set; }
    public int? TaskID { get; set; }
    public required ToDoProject Project { get; set; }
    public ToDoTask? ParentTask { get; set; }

    [StringLength(32)]
    public string Name { get; set; } = string.Empty;

    [StringLength(4)]
    public string? Icon { get; set; }
    public string? Text { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public DateTime? DueTime { get; set; }
    public bool IsCompleted { get; set; } = false;
    public bool IsStarred { get; set; } = false;
    public uint? Colour { get; set; }
    public ToDoTaskImportance Importance { get; set; } = ToDoTaskImportance.Regular;
}
