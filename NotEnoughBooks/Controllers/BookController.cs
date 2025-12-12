using ConstructorGenerator.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.UseCases.Interfaces;
using NotEnoughBooks.ViewModels;

namespace NotEnoughBooks.Controllers;

[GenerateFullConstructor]
[Authorize]
public partial class BookController : Controller
{
    private readonly ILogger<BookController> _logger;
    private readonly IRequestBookUseCase _requestBookUseCase;
    private readonly ISaveBookUseCase _saveBookUseCase;
    private readonly IGetBooksByUserUseCase _getBooksByUserUseCase;
    private readonly IGetBookUseCase _getBookUseCase;
    private readonly ISearchUseCase _searchUseCase;
    private readonly UserManager<IdentityUser> _userManager;
    
    [HttpGet]
    public IActionResult AddBook()
    {
        try
        {
            return View();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured");
            return StatusCode(500);
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetBookInfo(string query)
    {
        try
        {
            BookResult result = await _requestBookUseCase.Execute(query);
            
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

    public async Task<IActionResult> Index(IndexBookViewModel viewModel)
    {
        try
        {
            IdentityUser requestingUser = await GetRequestingUser();
            if (string.IsNullOrEmpty(viewModel.SearchText))
            {
                IEnumerable<Book> books = _getBooksByUserUseCase.Execute(viewModel.Order, viewModel.OrderAsc, requestingUser);
                return View(IndexBookViewModel.Create(books, viewModel.Order, viewModel.OrderAsc));
            }
            IEnumerable<Book> searchResult = _searchUseCase.Execute(viewModel.SearchText, viewModel.Order, viewModel.OrderAsc, requestingUser);
            return View(IndexBookViewModel.Create(searchResult, viewModel.Order, viewModel.OrderAsc, viewModel.SearchText));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured");
            return StatusCode(500);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            IdentityUser requestingUser = await GetRequestingUser();
            BookResult bookResult = await _getBookUseCase.Execute(id, requestingUser);
            return View(bookResult);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured");
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Book book)
    {
        try
        {
            IdentityUser requestingUser = await GetRequestingUser();
            bool execute = await _saveBookUseCase.Execute(book, requestingUser);
            if (!execute)
                return NotFound();
            
            return RedirectToAction(nameof(Index));
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