using System.ComponentModel.DataAnnotations.Schema;
using UniHelp.Domain.Common;

namespace UniHelp.Domain.Entities;

public class AnswerVariant : BaseEntity
{
    public string Text { get; set; }

    public bool IsCorrect { get; set; }
    
    [ForeignKey(nameof(Question))]
    public int QuestionId { get; set; }
    
    public TestQuestion Question { get; set; }
}