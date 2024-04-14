using Microsoft.AspNetCore.Mvc;
using ToDoAndBeyond.Interfaces;
using ToDoAndBeyond.Models;
using ToDoAndBeyond.Models.ControllerModels;

namespace ToDoAndBeyond.Controllers;

public class ProjectsController(
    ILogger<ProjectsController> logger,
    IToDoProjectRepository projectRepository,
    IToDoTaskRepository taskRepository
) : Controller
{
    private readonly ILogger<ProjectsController> logger = logger;
    private readonly IEnumerable<IEnumerable<ToDoProject>> projectChunks =
    [
        projectRepository.GetToDoProjects().Result.Where(project => project.Name.StartsWith('~')),
        projectRepository.GetToDoProjects().Result.Where(project => !project.Name.StartsWith('~')),
    ];

    [HttpGet("Projects")]
    public ActionResult Index() =>
        View(new ProjectsControllerModel() { ProjectChunks = projectChunks, });

    [HttpGet("Projects/Project{projectID:int}/{projectName}")]
    public ActionResult Index(int projectID, string projectName) =>
        View(
            new ProjectsControllerModel()
            {
                ProjectChunks = projectChunks,
                Tasks = taskRepository.GetToDoTasks(projectID, projectName).Result
            }
        );
}
