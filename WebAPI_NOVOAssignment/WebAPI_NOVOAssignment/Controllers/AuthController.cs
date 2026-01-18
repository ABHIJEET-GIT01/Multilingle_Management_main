using Microsoft.AspNetCore.Mvc;
using WebAPI_NOVOAssignment.DTOs;
using WebAPI_NOVOAssignment.Extensions;
using WebAPI_NOVOAssignment.Services;
using WebAPI_NOVOAssignment.Services.Interfaces;

namespace WebAPI_NOVOAssignment.Controllers;

/// <summary>
/// Authentication controller for login and token management
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILocalizationService _localizationService;

    public AuthController(IAuthService authService, ILocalizationService localizationService)
    {
        _authService = authService;
        _localizationService = localizationService;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponseDto<AuthResultDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseDto<object>), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponseDto<AuthResultDto>>> Login([FromBody] LoginRequestDto request)
    {
        var culture = HttpContext.GetCulture();

        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDto<object>
            {
                Success = false,
                Message = _localizationService.GetMessage("invalid_request", culture)
            });
        }

        var result = await _authService.LoginAsync(request);
        if (result == null)
        {
            return Unauthorized(new ApiResponseDto<object>
            {
                Success = false,
                Message = _localizationService.GetMessage("login_failed", culture)
            });
        }

        return Ok(new ApiResponseDto<AuthResultDto>
        {
            Success = true,
            Message = _localizationService.GetMessage("login_success", culture),
            Data = result
        });
    }

  
    [HttpPost("refresh")]
    [ProducesResponseType(typeof(ApiResponseDto<AuthResultDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponseDto<object>), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ApiResponseDto<AuthResultDto>>> Refresh([FromBody] RefreshTokenRequestDto request)
    {
        var culture = HttpContext.GetCulture();

        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponseDto<object>
            {
                Success = false,
                Message = _localizationService.GetMessage("invalid_request", culture)
            });
        }

        var result = await _authService.RefreshTokenAsync(request.RefreshToken);
        if (result == null)
        {
            return Unauthorized(new ApiResponseDto<object>
            {
                Success = false,
                Message = _localizationService.GetMessage("invalid_token", culture)
            });
        }

        return Ok(new ApiResponseDto<AuthResultDto>
        {
            Success = true,
            Message = _localizationService.GetMessage("token_refreshed", culture),
            Data = result
        });
    }

    [HttpPost("revoke")]
    [ProducesResponseType(typeof(ApiResponseDto<object>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponseDto<object>>> Revoke([FromBody] RefreshTokenRequestDto request)
    {
        var culture = HttpContext.GetCulture();
        var success = await _authService.RevokeTokenAsync(request.RefreshToken);
        
        return Ok(new ApiResponseDto<object>
        {
            Success = success,
            Message = success 
                ? _localizationService.GetMessage("token_revoked", culture)
                : _localizationService.GetMessage("revoke_failed", culture)
        });
    }
}
