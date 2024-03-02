using System.ComponentModel.DataAnnotations.Schema;
using UniHelp.Domain.Common;

namespace UniHelp.Domain.Entities;

public class Class : BaseEntity
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    [ForeignKey(nameof(Teacher))]
    public int TeacherId { get; set; }
    
    public Teacher Teacher { get; set; }
    
    public int ClassesNumber { get; set; }
    
    public virtual IList<StudentClass> StudentClasses { get; set; } = default!;
    
    public virtual IList<Task> Tasks { get; set; } = default!;
}