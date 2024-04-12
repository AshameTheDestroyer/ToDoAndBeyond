using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoAndBeyond.Models;

public class ToDoStep
{
    [Key]
    public int ID { get; set; }

    [ForeignKey(nameof(ToDoTask))]
    public required int TaskID { get; set; }
    public ToDoTask? Task { get; set; }

    [StringLength(32)]
    public required string Name { get; set; }
    public string? Text { get; set; }
    public bool IsCompleted { get; set; } = false;
}
