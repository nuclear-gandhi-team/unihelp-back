using System.ComponentModel.DataAnnotations.Schema;
using UniHelp.Domain.Common;
using UniHelp.Domain.Enums;
using TaskEntity = UniHelp.Domain.Entities.Task;

namespace UniHelp.Domain.Entities;

public class Comment : BaseEntity
{
    public string Body { get; set; } = default!;

    [ForeignKey(nameof(Entities.Task))]
    public int TaskId { get; set; }

    public TaskEntity Task { get; set; } = default!;

    [ForeignKey(nameof(User))]
    public string UserId { get; set; } = default!;

    public User User { get; set; } = default!;

    [ForeignKey(nameof(ParentComment))]
    public int? ParentCommentId { get; set; }

    public Comment? ParentComment { get; set; }
    
    public CommentTypes Action { get; set; }

    public bool IsRemoved { get; set; }

    public IEnumerable<Comment> SubComments { get; set; } = default!;
}