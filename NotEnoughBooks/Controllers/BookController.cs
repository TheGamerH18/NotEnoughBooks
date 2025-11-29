using ConstructorGenerator.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.UseCases.Interfaces;
using NotEnoughBooks.ViewModels;

namespace NotEnoughBooks.Controllers;

[GenerateFullConstructor]
[Route("[controller]/[action]")]
public partial class BookController : Controller
{
    private readonly ILogger<BookController> _logger;
    private readonly IRequestBookUseCase _requestBookUseCase;
    private readonly ISaveBookUseCase _saveBookUseCase;
    private readonly IGetBooksByUserUseCase _getBooksByUserUseCase;
    private readonly UserManager<IdentityUser> _userManager;
    
    [Authorize]
    [HttpGet("{isbn?}")]
    public IActionResult AddBook(string isbn = "")
    {
        try
        {
            return View(new BookSearchViewModel() { Isbn = isbn });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured");
            return StatusCode(500);
        }
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> GetBookInfo(BookSearchViewModel query)
    {
        try
        {
            BookResult result = await _requestBookUseCase.Execute(query.Isbn);
            
            if (result.Success)
                return View(result.Book);
            
            return BadRequest(result.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured");
            return StatusCode(500);
        }
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> SaveBook(Book book)
    {
        try
        {
            IdentityUser requestingUser = await GetRequestingUser();
            return View(await _saveBookUseCase.Execute(book, requestingUser));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured");
            return StatusCode(500);
        }
    }

    public async Task<IActionResult> Index()
    {
        try
        {
            IdentityUser requestingUser = await GetRequestingUser();
            IEnumerable<Book> books = _getBooksByUserUseCase.Execute(requestingUser);
            return View(books);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured");
            return StatusCode(500);
        }
    }

    private async Task<IdentityUser> GetRequestingUser()
    {
        return await _userManager.GetUserAsync(HttpContext.User);
    }
}