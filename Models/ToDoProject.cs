using System.ComponentModel.DataAnnotations;

namespace ToDoAndBeyond.Models;

public class ToDoProject
{
    [Key]
    public int ID { get; set; }
    public int UserID { get; set; }

    [StringLength(32)]
    public string Name { get; set; } = string.Empty;

    [StringLength(4)]
    public string? Icon { get; set; }
    public string? Text { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime UpdateTime { get; set; }
}
