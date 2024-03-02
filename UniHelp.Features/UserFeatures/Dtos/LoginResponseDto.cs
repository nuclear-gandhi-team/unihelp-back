using System.ComponentModel.DataAnnotations;

namespace UniHelp.Features.UserFeatures.Dtos;

public record LoginResponseDto
{
    [Required]
    public string Token { get; set; } = default!;
}