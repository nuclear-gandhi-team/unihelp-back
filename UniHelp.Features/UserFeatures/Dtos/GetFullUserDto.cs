using System.ComponentModel.DataAnnotations;

namespace UniHelp.Features.UserFeatures.Dtos;

public record GetFullUserDto
{
    [Required]
    public string Id { get; set; } = default!;

    [Required]
    public string FirstName { get; set; } = default!;

    [Required]
    public string LastName { get; set; } = default!;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;
}