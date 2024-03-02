using System.ComponentModel.DataAnnotations;
using UniHelp.Domain.Enums;

namespace UniHelp.Features.CommentFeatures;

public class CreateCommentDto
{
    [Required]
    public int TaskId { get; set; }
    
    [Required]
    public CommentTypes Action { get; set; }

    public int? ParentCommentId { get; set; }

    [Required]
    public string Body { get; set; } = default!;
}