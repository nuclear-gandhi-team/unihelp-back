using UniHelp.Domain.Enums;

namespace UniHelp.Features.TasksFeature.Dtos;

public class GetTaskDto
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public int MaxPoints { get; set; }
    
    public DateTime DateStart { get; set; }
    
    public DateTime DateEnd { get; set; }

    public int ClassId { get; set; }
    
    public TaskType Type { get; set; }
}