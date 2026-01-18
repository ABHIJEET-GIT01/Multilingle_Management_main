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

    /// <summary>
    /// Register a new user
    /// </summary>
    /// <remarks>
    /// Query parameters:
    /// - culture: Language code (en, hi, mr) - default: en
    /// </remarks>
    [HttpPost("register")]
    [ProducesResponseType(typeof(ApiResponseDto<UserDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseDto<object>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponseDto<UserDetailDto>>> Register([FromBody] CreateUserRequestDto request)
    {
        var culture = HttpContext.GetCulture();

        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDto<object>
            {
                Success = false,
                Message = _localizationService.GetMessage("validation_failed", culture),
                Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()
            });
        }

        var user = await _userService.CreateUserAsync(request);
        if (user == null)
        {
            return Conflict(new ApiResponseDto<object>
            {
                Success = false,
                Message = _localizationService.GetMessage("registration_failed", culture)
            });
        }

        return Ok(new ApiResponseDto<UserDetailDto>
        {
            Success = true,
            Message = _localizationService.GetMessage("registration_success", culture),
            Data = user
        });
    }
}
