using System.ComponentModel.DataAnnotations.Schema;
using UniHelp.Domain.Common;

namespace UniHelp.Domain.Entities;

public class TestQuestion : BaseEntity
{
    [ForeignKey(nameof(Task))]
    public int TaskId { get; set; }
    
    public Task Task { get; set; }
    
    public string Question { get; set; }
    
    public virtual IList<AnswerVariant> AnswerVariants { get; set; } = default!;
}