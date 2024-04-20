using Extensions;
using Microsoft.AspNetCore.Mvc;
using ToDoAndBeyond.DTOs;
using ToDoAndBeyond.Interfaces;
using ToDoAndBeyond.Models;

namespace ToDoAndBeyond.Controllers;

public class ProjectsController(
    ILogger<ProjectsController> logger,
    IToDoProjectRepository projectRepository,
    IToDoTaskRepository taskRepository,
    IToDoStepRepository stepRepository
) : Controller
{
    private const string baseURL = "/Projects";
    private const string projectURL = baseURL + "/Project{projectID}/{projectName}";
    private const string taskURL = projectURL + "/Task{taskID}";

    private readonly ILogger<ProjectsController> logger = logger;
    private readonly IEnumerable<IEnumerable<ToDoProject>> projectChunks =
    [
        projectRepository.GetToDoProjects().Result.Where(project => project.Name.StartsWith('~')),
        projectRepository.GetToDoProjects().Result.Where(project => !project.Name.StartsWith('~')),
    ];

    public static string GenerateURL(int projectID, string projectName, int? taskID = null) =>
        (taskID == null ? projectURL : taskURL).Format(
            new
            {
                projectID,
                projectName = projectName.Replace(" ", ""),
                taskID,
            }
        );

    [HttpGet(baseURL)]
    public ActionResult Index() =>
        View(
            new ProjectsDTO
            {
                ProjectChunks = projectChunks.Select(projectChunk =>
                    projectChunk.Select(project => project.MapToDTO())
                )
            }
        );

    [HttpGet(taskURL)]
    [HttpGet(projectURL)]
    public ActionResult Index(int projectID, string projectName, int? taskID = null) =>
        View(
            new ProjectsDTO
            {
                SelectedTask =
                    taskID != null
                        ? taskRepository.GetToDoTask(taskID.Value).Result?.MapToDTO()
                        : null,
                SelectedProject = projectRepository.GetToDoProject(projectID).Result?.MapToDTO(),
                ProjectChunks = projectChunks.Select(projectChunk =>
                    projectChunk.Select(project => project.MapToDTO())
                ),
                Tasks = taskRepository
                    .GetToDoTasks(projectID, projectName)
                    .Result.Select(task => task.MapToDTO()),
                Steps =
                    taskID != null
                        ? stepRepository
                            .GetToDoSteps(taskID.Value)
                            .Result.Select(step => step.MapToDTO())
                        : null,
            }
        );
}
