using System.ComponentModel.DataAnnotations.Schema;
using UniHelp.Domain.Common;

namespace UniHelp.Domain.Entities;

public class StudentClass : BaseEntity
{
    [ForeignKey(nameof(Student))]
    public int StudentId { get; set; }
    
    public Student Student { get; set; }
    
    [ForeignKey(nameof(Class))]
    public int ClassId { get; set; }
    
    public Class Class { get; set; }
}