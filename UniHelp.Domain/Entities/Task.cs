using System.ComponentModel.DataAnnotations.Schema;
using UniHelp.Domain.Common;
using UniHelp.Domain.Enums;

namespace UniHelp.Domain.Entities;

public class Task : BaseEntity
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public int MaxPoints { get; set; }
    
    public DateTime DateStart { get; set; }
    
    public DateTime DateEnd { get; set; }
    
    [ForeignKey(nameof(Class))]
    public int ClassId { get; set; }
    
    public Class Class { get; set; }
    
    public TaskType Type { get; set; }
    
    public virtual IList<StudentTask> StudentTasks { get; set; } = default!;
    
    public virtual IList<TestQuestion> TestQuestions { get; set; } = default!;
    
    public virtual IList<Comment> Comments { get; set; } = default!;
}