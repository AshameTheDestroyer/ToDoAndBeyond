using Microsoft.AspNetCore.Mvc;
using ToDoAndBeyond.Interfaces;
using ToDoAndBeyond.Models;

namespace ToDoAndBeyond.Controllers;

public class ProjectsController(
    ILogger<ProjectsController> logger,
    IToDoProjectRepository projectRepository
) : Controller
{
    private readonly ILogger<ProjectsController> logger = logger;
    private readonly IToDoProjectRepository projectRepository = projectRepository;

    [HttpGet("Projects")]
    public async Task<ActionResult> Index()
    {
        var projects = await projectRepository.GetToDoProjects();
        return View(
            new IEnumerable<ToDoProject>[]
            {
                projects
                    .Where(project => project.Name.StartsWith('~'))
                    .Select(project => project with { Name = project.Name.Remove(0, 1) }),
                projects.Where(project => !project.Name.StartsWith('~')),
            }
        );
    }
}
