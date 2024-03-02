using UniHelp.Features.ClassFeatures.Dtos;

namespace UniHelp.Services.Interfaces;

public interface IClassService
{
    Task<IEnumerable<GetClassDto>> GetAllTeacherClassesAsync(int teacherId);
    
    Task<GetClassDto> GetClassByIdAsync(int id);
    
    Task<GetClassDto> CreateClassAsync(AddClassDto newClass, int teacherId);
    
    Task<GetClassDto> AddStudentToClassAsync(int classId, string email);
}