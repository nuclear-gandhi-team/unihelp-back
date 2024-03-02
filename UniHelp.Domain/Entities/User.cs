using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace UniHelp.Domain.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }

    [ForeignKey(nameof(Student))]
    public int StudentId { get; set; }
    
    public virtual Student? Student { get; set; }

    [ForeignKey(nameof(Teacher))]
    public int TeacherId { get; set; }
    
    public virtual Teacher? Teacher { get; set; }
}