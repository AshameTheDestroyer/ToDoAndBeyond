using System.ComponentModel.DataAnnotations;

namespace ToDoAndBeyond.Models;

public class ToDoStep
{
    [Key]
    public int ID { get; set; }
    public int TaskID { get; set; }
    public required ToDoTask Task { get; set; }

    [StringLength(32)]
    public string Name { get; set; } = string.Empty;
    public string? Text { get; set; }
    public bool IsCompleted { get; set; } = false;
}
