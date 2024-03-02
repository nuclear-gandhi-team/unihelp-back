using System.ComponentModel.DataAnnotations.Schema;
using UniHelp.Domain.Common;

namespace UniHelp.Domain.Entities;

public class StudentTask : BaseEntity
{
    [ForeignKey(nameof(Student))]
    public int StudentId { get; set; }
    
    public Student Student { get; set; }
    
    [ForeignKey(nameof(Task))]
    public int TaskId { get; set; }
    
    public Task Task { get; set; }
    
    public int Grade { get; set; }
    
    public DateTime HandedDate { get; set; }
    
    public byte[]? File { get; set; } = default!;
}