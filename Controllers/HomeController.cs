using Microsoft.AspNetCore.Mvc;

namespace ToDoAndBeyond.Controllers;

public class HomeController(ILogger<HomeController> logger) : Controller
{
    private readonly ILogger<HomeController> logger = logger;

    public IActionResult Index() => View();
}
