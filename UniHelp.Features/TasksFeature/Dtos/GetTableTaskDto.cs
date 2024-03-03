using UniHelp.Domain.Enums;

namespace UniHelp.Features.TasksFeature.Dtos;

public class GetTableTaskDto
{
    public string Name { get; set; }

    public int MaxPoints { get; set; }

    public DateTime DateEnd { get; set; }

    public string ClassName { get; set; }
    
    public TaskType Type { get; set; }
}