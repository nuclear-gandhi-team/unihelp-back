using UniHelp.Domain.Common;

namespace UniHelp.Domain.Entities;

public class Teacher : BaseEntity
{
    public virtual IList<Class> Classes { get; set; } = default!;
    
    public virtual User User { get; set; } = default!;
}