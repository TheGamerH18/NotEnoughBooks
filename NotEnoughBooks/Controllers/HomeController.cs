using System.Diagnostics;
using ConstructorGenerator.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotEnoughBooks.Core.Models;
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

    [Authorize]
    public async Task<IActionResult> AddBook()
    {
        try
        {
            BookResult result = await _requestBookUseCase.Execute("978-3-7539-1693-4");
            
            if (result.Success)
                return Ok(result.Book);
            
            return BadRequest(result.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured");
            return StatusCode(500);
        }
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
