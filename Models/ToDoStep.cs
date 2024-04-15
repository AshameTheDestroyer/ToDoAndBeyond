using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoAndBeyond.DTOs;

namespace ToDoAndBeyond.Models;

public record ToDoStep
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

public static class ToDoStepDTOMappingExtension
{
    public static ToDoStepDTO MapToDTO(this ToDoStep step) =>
        new()
        {
            ID = step.ID,
            Name = step.Name,
            IsCompleted = step.IsCompleted,
        };
}
