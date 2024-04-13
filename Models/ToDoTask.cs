using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoAndBeyond.Enums;

namespace ToDoAndBeyond.Models;

public record ToDoTask
{
    [Key]
    public int ID { get; set; }

    [ForeignKey(nameof(ToDoProject))]
    public required int ProjectID { get; set; }

    [ForeignKey(nameof(ToDoTask))]
    public int? ParentTaskID { get; set; }
    public ToDoProject? Project { get; set; }
    public ToDoTask? ParentTask { get; set; }

    [StringLength(32)]
    public required string Name { get; set; }

    [StringLength(4)]
    public string? Icon { get; set; }
    public string? Text { get; set; }
    public DateTime CreationTime { get; set; } = DateTime.Now;
    public DateTime UpdateTime { get; set; } = DateTime.Now;
    public DateTime? DueTime { get; set; }
    public bool IsCompleted { get; set; } = false;
    public bool IsStarred { get; set; } = false;
    public uint? Colour { get; set; }
    public ToDoTaskImportance Importance { get; set; } = ToDoTaskImportance.Regular;
}
