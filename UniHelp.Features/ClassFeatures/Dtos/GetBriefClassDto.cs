namespace UniHelp.Features.ClassFeatures.Dtos;

public class GetBriefClassDto
{
    public int ClassId { get; set; }
    
    public string ClassName { get; set; }

    public int ClassesNumber { get; set; }
    
    public int StudentsCount { get; set; }
}