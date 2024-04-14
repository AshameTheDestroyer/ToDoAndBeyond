using Microsoft.AspNetCore.Mvc;
using ToDoAndBeyond.DTOs;
using ToDoAndBeyond.Interfaces;
using ToDoAndBeyond.Models;

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
        View(
            new ProjectsDTO
            {
                ProjectChunks = projectChunks.Select(projectChunk =>
                    projectChunk.Select(project => project.MapToDTO())
                )
            }
        );

    [HttpGet("Projects/Project{projectID:int}/{projectName}")]
    public ActionResult Index(int projectID, string projectName) =>
        View(
            new ProjectsDTO
            {
                ProjectChunks = projectChunks.Select(projectChunk =>
                    projectChunk.Select(project => project.MapToDTO())
                ),
                Tasks = taskRepository
                    .GetToDoTasks(projectID, projectName)
                    .Result.Select(task => task.MapToDTO())
            }
        );
}
