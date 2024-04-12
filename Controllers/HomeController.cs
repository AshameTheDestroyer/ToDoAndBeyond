using Microsoft.AspNetCore.Mvc;
using ToDoAndBeyond.Interfaces;

namespace ToDoAndBeyond.Controllers;

public class HomeController(
    ILogger<HomeController> logger,
    IToDoProjectRepository projectRepository,
    IToDoTaskRepository taskRepository,
    IToDoStepRepository stepRepository
) : Controller
{
    private readonly ILogger<HomeController> logger = logger;
    private readonly IToDoProjectRepository projectRepository = projectRepository;
    private readonly IToDoTaskRepository taskRepository = taskRepository;
    private readonly IToDoStepRepository stepRepository = stepRepository;

    public async Task<IActionResult> Index()
    {
        var projects = await projectRepository.GetToDoProjects();
        var tasks = await taskRepository.GetToDoTasks();
        var steps = await stepRepository.GetToDoSteps();

        logger.Log(LogLevel.Information, $"ProjectCount: {projects.Count()}");
        logger.Log(LogLevel.Information, $"TaskCount: {tasks.Count()}");
        logger.Log(LogLevel.Information, $"StepCount: {steps.Count()}");

        return View("Authentication");
    }
}
