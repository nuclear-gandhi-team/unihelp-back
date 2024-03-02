using UniHelp.Domain.Common;

namespace UniHelp.Domain.Entities;

public class Student : BaseEntity
{
    public string Faculty { get; set; }
    
    public string Course { get; set; }
    
    public string Group { get; set; }
    
    public virtual IList<StudentTask> StudentTasks { get; set; } = default!;
    
    public virtual IList<StudentClass> StudentClasses { get; set; } = default!;

    public virtual User User { get; set; } = default!;
}