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
    public const string baseURL = "/Projects";
    public const string projectURL = baseURL + "/Project{projectID}/{projectName}";
    public const string taskURL = projectURL + "/Task{taskID}";
    public const string searchProjectURL = baseURL + "/Search/{term}";

    private readonly ILogger<ProjectsController> logger = logger;
    private readonly IEnumerable<IEnumerable<ToDoProject>> projectChunks =
    [
        projectRepository.GetToDoProjects().Result.Where(project => project.Name.StartsWith('~')),
        projectRepository.GetToDoProjects().Result.Where(project => !project.Name.StartsWith('~')),
    ];

    public static string GenerateURL(int projectID, string projectName, int? taskID = null)
    {
        return (taskID == null ? projectURL : taskURL).Format(
            new
            {
                projectID,
                projectName = projectName.Replace(" ", ""),
                taskID,
            }
        );
    }

    public static string GenerateURL(string term) => searchProjectURL.Format(new { term });

    [HttpGet(baseURL)]
    public ActionResult Index()
    {
        return View(
            new ProjectsDTO
            {
                ProjectChunks = projectChunks.Select(projectChunk =>
                    projectChunk.Select(project => project.MapToDTO())
                )
            }
        );
    }

    [HttpGet(taskURL)]
    [HttpGet(projectURL)]
    public ActionResult Index(int projectID, string projectName, int? taskID = null)
    {
        return View(
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

    public ActionResult Search() => RedirectToAction(nameof(Index));

    [HttpGet(searchProjectURL)]
    public ActionResult Search(string term)
    {
        return View(
            "Index",
            new ProjectsDTO
            {
                SearchTerm = term,
                ProjectChunks = projectChunks.Select(projectChunk =>
                    projectChunk
                        .Where(project => project.Name.ToLower().Contains(term.Trim().ToLower()))
                        .Select(project => project.MapToDTO())
                ),
            }
        );
    }
}
