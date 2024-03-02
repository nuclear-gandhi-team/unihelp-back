using System.ComponentModel.DataAnnotations;

namespace UniHelp.Features.UserFeatures.Dtos;

public record UpdateUserDto
{
    [Required] public string Id { get; set; } = default!;

    public string? NewFirstName { get; set; } = default!;
    
    public string? NewLastName { get; set; } = default!;
}