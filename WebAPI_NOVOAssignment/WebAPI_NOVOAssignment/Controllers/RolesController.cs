using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI_NOVOAssignment.DTOs;
using WebAPI_NOVOAssignment.Extensions;
using WebAPI_NOVOAssignment.Services;
using WebAPI_NOVOAssignment.Services.Interfaces;

namespace WebAPI_NOVOAssignment.Controllers;

/// <summary>
/// Role management controller for CRUD operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly ILocalizationService _localizationService;

    public RolesController(IRoleService roleService, ILocalizationService localizationService)
    {
        _roleService = roleService;
        _localizationService = localizationService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiListResponseDto<RoleDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiListResponseDto<RoleDetailDto>>> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var culture = HttpContext.GetCulture();
        var (roles, totalCount) = await _roleService.GetAllRolesAsync(pageNumber, pageSize);

        return Ok(new ApiListResponseDto<RoleDetailDto>
        {
            Success = true,
            Message = _localizationService.GetMessage("roles_loaded", culture),
            Data = roles,
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        });
    }
    
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseDto<RoleDetailDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponseDto<RoleDetailDto>>> GetById(Guid id)
    {
        var culture = HttpContext.GetCulture();
        var role = await _roleService.GetRoleByIdAsync(id);
        
        if (role == null)
        {
            return NotFound(new ApiResponseDto<object>
            {
                Success = false,
                Message = _localizationService.GetMessage("role_not_found", culture)
            });
        }

        return Ok(new ApiResponseDto<RoleDetailDto>
        {
            Success = true,
            Message = _localizationService.GetMessage("roles_loaded", culture),
            Data = role
        });
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponseDto<RoleDetailDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ApiResponseDto<RoleDetailDto>>> Create([FromBody] CreateRoleRequestDto request)
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

        var role = await _roleService.CreateRoleAsync(request);
        if (role == null)
        {
            return Conflict(new ApiResponseDto<object>
            {
                Success = false,
                Message = _localizationService.GetMessage("validation_failed", culture)
            });
        }

        return CreatedAtAction(nameof(GetById), new { id = role.Id }, new ApiResponseDto<RoleDetailDto>
        {
            Success = true,
            Message = _localizationService.GetMessage("role_created", culture),
            Data = role
        });
    }

   
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponseDto<RoleDetailDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponseDto<RoleDetailDto>>> Update(Guid id, [FromBody] UpdateRoleRequestDto request)
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

        var role = await _roleService.UpdateRoleAsync(id, request);
        if (role == null)
        {
            return Conflict(new ApiResponseDto<object>
            {
                Success = false,
                Message = _localizationService.GetMessage("role_not_found", culture)
            });
        }

        return Ok(new ApiResponseDto<RoleDetailDto>
        {
            Success = true,
            Message = _localizationService.GetMessage("role_updated", culture),
            Data = role
        });
    }

   
    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(typeof(ApiResponseDto<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponseDto<object>>> Delete(Guid id)
    {
        var culture = HttpContext.GetCulture();
        var success = await _roleService.DeleteRoleAsync(id);
        
        if (!success)
        {
            return NotFound(new ApiResponseDto<object>
            {
                Success = false,
                Message = _localizationService.GetMessage("role_not_found", culture)
            });
        }

        return Ok(new ApiResponseDto<object>
        {
            Success = true,
            Message = _localizationService.GetMessage("role_deleted", culture)
        });
    }
}
