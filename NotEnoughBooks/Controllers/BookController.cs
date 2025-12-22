using ConstructorGenerator.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NotEnoughBooks.Core.Extensions;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.UseCases.Interfaces;
using NotEnoughBooks.ViewModels;

namespace NotEnoughBooks.Controllers;

[GenerateFullConstructor]
[Authorize]
public partial class BookController : Controller
{
    private readonly ILogger<BookController> _logger;
    private readonly IRequestNewBookUseCase _requestNewBookUseCase;
    private readonly ISaveBookUseCase _saveBookUseCase;
    private readonly IGetBooksByUserUseCase _getBooksByUserUseCase;
    private readonly IGetBookUseCase _getBookUseCase;
    private readonly ISearchUseCase _searchUseCase;
    private readonly IDeleteBookUseCase _deleteBookUseCase;
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
    public async Task<IActionResult> CreateBook(string query)
    {
        try
        {
            BookParserResult parserResult = await _requestNewBookUseCase.Execute(query);
            if (!parserResult.Success)
                return BadRequest(parserResult.ErrorMessage);

            BookFormViewModel bookFormViewModel = BookFormViewModel.Create(parserResult.Book);
            return View(bookFormViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured");
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> SaveBook(BookFormViewModel bookFormViewModel)
    {
        try
        {
            IdentityUser requestingUser = await GetRequestingUser();
            string fileExtension = bookFormViewModel.Image?.ContentType.GetFileExtension();

            if (fileExtension != null)
                return View(await _saveBookUseCase.Execute(bookFormViewModel.Book,
                                                           requestingUser,
                                                           bookFormViewModel.Image.OpenReadStream(),
                                                           fileExtension));

            return View(await _saveBookUseCase.Execute(bookFormViewModel.Book, requestingUser));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured");
            return StatusCode(500);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Index(IndexBookViewModel viewModel)
    {
        try
        {
            IdentityUser requestingUser = await GetRequestingUser();
            IEnumerable<Book> books;
            if (string.IsNullOrEmpty(viewModel.SearchText))
            {
                books = _getBooksByUserUseCase.Execute(viewModel.Order,
                                                       viewModel.OrderAsc,
                                                       requestingUser).ToArray();
            }
            else
            {
                books = _searchUseCase.Execute(viewModel.SearchText,
                                               viewModel.Order,
                                               viewModel.OrderAsc,
                                               requestingUser).ToArray();
            }

            int pageAmount = (int)Math.Ceiling((decimal)books.Count() / 30);
            if (viewModel.View != IndexBookViewModel.ViewMode.Grid)
                return View(IndexBookViewModel.Create(books, 0, viewModel));

            books = books.Skip((viewModel.Page - 1) * 30).Take(30);
            return View(IndexBookViewModel.Create(books, pageAmount, viewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured");
            return StatusCode(500);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Edit(Guid id)
    {
        try
        {
            IdentityUser requestingUser = await GetRequestingUser();
            BookResult bookParserResult = await _getBookUseCase.Execute(id, requestingUser);

            EditViewModel editViewModel = EditViewModel.Create(bookParserResult);
            return View(editViewModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured");
            return StatusCode(500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(BookFormViewModel bookFormViewModel)
    {
        try
        {
            IdentityUser requestingUser = await GetRequestingUser();

            string fileExtension = bookFormViewModel.Image?.ContentType.GetFileExtension();
            bool execute;
            if (fileExtension != null)
                execute = await _saveBookUseCase.Execute(bookFormViewModel.Book,
                                                         requestingUser,
                                                         bookFormViewModel.Image.OpenReadStream(),
                                                         fileExtension);
            else
                execute = await _saveBookUseCase.Execute(bookFormViewModel.Book, requestingUser);

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

    [HttpGet]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            IdentityUser requestingUser = await GetRequestingUser();
            await _deleteBookUseCase.Execute(id, requestingUser);
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