namespace WebAPI_NOVOAssignment.DTOs;

/// <summary>
/// Generic API response wrapper for single item responses
/// </summary>
/// <typeparam name="T">Type of data being returned</typeparam>
public class ApiResponseDto<T>
{
    public bool Success { get; set; }
    public required string Message { get; set; }

    /// <summary>
    /// The actual data returned
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// List of validation errors if operation failed
    /// </summary>
    public List<string> Errors { get; set; } = [];
}
