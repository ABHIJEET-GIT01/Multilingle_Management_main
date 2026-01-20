using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI_NOVOAssignment.DTOs;
using WebAPI_NOVOAssignment.Extensions;
using WebAPI_NOVOAssignment.Services;
using WebAPI_NOVOAssignment.Services.Interfaces;
using WebAPI_NOVOAssignment.Utilities;

namespace WebAPI_NOVOAssignment.Controllers;

/// <summary>
/// Forms controller for retrieving form structures and registration
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class FormsController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILocalizationService _localizationService;

    public FormsController(IUserService userService, ILocalizationService localizationService)
    {
        _userService = userService;
        _localizationService = localizationService;
    }

    [HttpGet("login-form")]
    [ProducesResponseType(typeof(FormStructureDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<FormStructureDto> GetLoginForm()
    {
        var culture = HttpContext.GetCulture();
        var form = FormStructureProvider.GetFormStructure("login", culture);
        
        if (form == null)
        {
            return NotFound(new { message = _localizationService.GetMessage("form_not_found", culture) });
        }

        return Ok(form);
    }

    [HttpGet("register-form")]
    [ProducesResponseType(typeof(FormStructureDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<FormStructureDto> GetRegisterForm()
    {
        var culture = HttpContext.GetCulture();
        var form = FormStructureProvider.GetFormStructure("register", culture);
        
        if (form == null)
        {
            return NotFound(new { message = _localizationService.GetMessage("form_not_found", culture) });
        }

        return Ok(form);
    }
}
