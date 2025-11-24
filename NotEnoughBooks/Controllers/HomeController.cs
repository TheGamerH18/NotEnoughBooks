using System.Diagnostics;
using ConstructorGenerator.Attributes;
using Microsoft.AspNetCore.Mvc;
using NotEnoughBooks.Core.UseCases.Interfaces;
using NotEnoughBooks.Models;

namespace NotEnoughBooks.Controllers;

[GenerateFullConstructor]
public partial class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRequestBookUseCase _requestBookUseCase;

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
