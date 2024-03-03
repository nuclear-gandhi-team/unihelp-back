using UniHelp.Features.ClassFeatures.Dtos;

namespace UniHelp.Services.Interfaces;

public interface IClassService
{
    Task<IEnumerable<GetClassDto>> GetClassesAsync(int teacherId);
    
    Task<IEnumerable<GetClassDto>> GetClassesAsync();
    
    Task<GetClassDto> GetClassByIdAsync(int id);
    
    Task<GetClassDto> CreateClassAsync(AddClassDto newClass, string userId);
    
    Task<GetClassDto> AddStudentToClassAsync(int classId, string email);
}