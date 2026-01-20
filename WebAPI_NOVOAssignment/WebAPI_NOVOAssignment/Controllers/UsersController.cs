using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI_NOVOAssignment.DTOs;
using WebAPI_NOVOAssignment.Extensions;
using WebAPI_NOVOAssignment.Services;
using WebAPI_NOVOAssignment.Services.Interfaces;

namespace WebAPI_NOVOAssignment.Controllers;

/// <summary>
/// User management controller for CRUD operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILocalizationService _localizationService;

    /// <summary>
    /// Initializes a new instance of the UsersController
    /// </summary>
    public UsersController(IUserService userService, ILocalizationService localizationService)
    {
        _userService = userService;
        _localizationService = localizationService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiListResponseDto<UserDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiListResponseDto<UserDetailDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var culture = HttpContext.GetCulture();
        var (users, totalCount) = await _userService.GetAllUsersAsync(pageNumber, pageSize);

        return Ok(new ApiListResponseDto<UserDetailDto>
        {
            Success = true,
            Message = _localizationService.GetMessage("users_loaded", culture),
            Data = users,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        });
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseDto<UserDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponseDto<UserDetailDto>>> GetById(Guid id)
    {
        var culture = HttpContext.GetCulture();
        var user = await _userService.GetUserByIdAsync(id);
        
        if (user == null)
        {
            return NotFound(new ApiResponseDto<object>
            {
                Success = false,
                Message = _localizationService.GetMessage("user_not_found", culture)
            });
        }

        return Ok(new ApiResponseDto<UserDetailDto>
        {
            Success = true,
            Message = _localizationService.GetMessage("users_loaded", culture),
            Data = user
        });
    }

    [HttpPost]
    //[Authorize(Roles = "Admin")]
    //[ProducesResponseType(typeof(ApiResponseDto<UserDetailDto>), StatusCodes.Status201Created)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
    //[ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<ApiResponseDto<UserDetailDto>>> Create([FromBody] CreateUserRequestDto request)
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
            return BadRequest(new ApiResponseDto<object>
            {
                Success = false,
                Message = _localizationService.GetMessage("registration_failed", culture)
            });
        }

        return CreatedAtAction(nameof(GetById), new { id = user.Id }, new ApiResponseDto<UserDetailDto>
        {
            Success = true,
            Message = _localizationService.GetMessage("user_created", culture),
            Data = user
        });
    }

   
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponseDto<UserDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponseDto<UserDetailDto>>> Update(Guid id, [FromBody] UpdateUserRequestDto request)
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

        var user = await _userService.UpdateUserAsync(id, request);
        if (user == null)
        {
            return NotFound(new ApiResponseDto<object>
            {
                Success = false,
                Message = _localizationService.GetMessage("user_not_found", culture)
            });
        }

        return Ok(new ApiResponseDto<UserDetailDto>
        {
            Success = true,
            Message = _localizationService.GetMessage("user_updated", culture),
            Data = user
        });
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponseDto<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponseDto<object>>> Delete(Guid id)
    {
        var culture = HttpContext.GetCulture();
        var success = await _userService.DeleteUserAsync(id);
        
        if (!success)
        {
            return NotFound(new ApiResponseDto<object>
            {
                Success = false,
                Message = _localizationService.GetMessage("user_not_found", culture)
            });
        }

        return Ok(new ApiResponseDto<object>
        {
            Success = true,
            Message = _localizationService.GetMessage("user_deleted", culture)
        });
    }
}
