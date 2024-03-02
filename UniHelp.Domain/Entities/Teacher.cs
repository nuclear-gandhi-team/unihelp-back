using UniHelp.Domain.Common;

namespace UniHelp.Domain.Entities;

public class Teacher : BaseEntity
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public virtual IList<Class> Classes { get; set; } = default!;
}