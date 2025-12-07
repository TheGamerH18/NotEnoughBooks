using ConstructorGenerator.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotEnoughBooks.Core.Models;
using NotEnoughBooks.Core.UseCases.Interfaces;

namespace NotEnoughBooks.Controllers;

[Authorize(Roles = "Admin")]
[Route("[controller]/[action]")]
[GenerateFullConstructor]
public partial class AdminController : Controller
{
    private readonly ICheckAdminConfigurationUseCase _checkAdminConfigurationUseCase;
    private readonly ISetAdminConfigurationUseCase _setAdminConfigurationUseCase;
    private readonly ILogger<AdminController> _logger;

    [HttpGet]
    public IActionResult Configuration()
    {
        try
        {
            return View(_checkAdminConfigurationUseCase.GetAdminConfiguration());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured");
            return StatusCode(500);
        }
    }

    public IActionResult Configuration(AdminConfiguration adminConfiguration)
    {
        try
        {
            _setAdminConfigurationUseCase.SetAdminConfiguration(adminConfiguration);
            return View(_checkAdminConfigurationUseCase.GetAdminConfiguration());
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured");
            return StatusCode(500);
        }
    }
}