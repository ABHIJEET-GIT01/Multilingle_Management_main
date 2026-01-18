namespace WebAPI_NOVOAssignment.DTOs;

/// <summary>
/// DTO representing a form field
/// </summary>
public class FormFieldDto
{
    public required string Name { get; set; }
    public required string Type { get; set; }
    public required string Label { get; set; }
    public string? Placeholder { get; set; }
    public bool Required { get; set; }
}

/// <summary>
/// DTO representing a form structure
/// </summary>
public class FormStructureDto
{
    public required string Title { get; set; }
    public List<FormFieldDto> Fields { get; set; } = [];
}
