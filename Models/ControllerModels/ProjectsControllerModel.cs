namespace ToDoAndBeyond.Models.ControllerModels;

public record ProjectsControllerModel
{
    public required IEnumerable<IEnumerable<ToDoProject>> ProjectChunks { get; set; }
    public IEnumerable<ToDoTask>? Tasks { get; set; }
}
