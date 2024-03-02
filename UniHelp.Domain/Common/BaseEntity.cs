using System.ComponentModel.DataAnnotations;

namespace UniHelp.Domain.Common;

public class BaseEntity
{
    [Key]
    public int Id { get; set; }
}