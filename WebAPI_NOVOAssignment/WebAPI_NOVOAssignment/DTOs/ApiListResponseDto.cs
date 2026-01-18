namespace WebAPI_NOVOAssignment.DTOs;

/// <summary>
/// Generic API response wrapper for list responses with pagination
/// </summary>
/// <typeparam name="T">Type of items in the list</typeparam>
public class ApiListResponseDto<T>
{
    /// <summary>
    /// Indicates if the operation was successful
    /// </summary>
    public bool Success { get; set; }
    public required string Message { get; set; }
    public List<T> Data { get; set; } = [];

    /// <summary>
    /// Total count of items (before pagination)
    /// </summary>
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<string> Errors { get; set; } = [];
}
