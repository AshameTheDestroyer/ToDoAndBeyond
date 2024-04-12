using Microsoft.AspNetCore.Mvc;

// using ToDoAndBeyond.Interfaces;

namespace ToDoAndBeyond.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> logger = logger;

    public IActionResult Index()
    {
        // var x = await userRepository.GetUsers();
        // logger.Log(LogLevel.Critical, $"UserCount: {x.Count()}");
        return View("Authentication");
    }
}
