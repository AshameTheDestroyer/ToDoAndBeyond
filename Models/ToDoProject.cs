using System.ComponentModel.DataAnnotations;

namespace ToDoAndBeyond.Models;

public record ToDoProject
{
    [Key]
    public int ID { get; set; }

    // public required int UserID { get; set; }

    [StringLength(32)]
    public required string Name { get; set; }

    [StringLength(4)]
    public string? Icon { get; set; }
    public string? Text { get; set; }
    public DateTime CreationTime { get; set; } = DateTime.Now;
    public DateTime UpdateTime { get; set; } = DateTime.Now;
}
