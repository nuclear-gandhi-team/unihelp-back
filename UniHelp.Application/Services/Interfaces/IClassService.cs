using UniHelp.Domain.Entities;
using UniHelp.Features.ClassFeatures.Dtos;

namespace UniHelp.Services.Interfaces;

public interface IClassService
{
    Task<IEnumerable<GetClassDto>> GetClassesAsync(int teacherId);
    
    Task<GetClassDto> GetClassByIdAsync(int id);
    
    Task<GetClassDto> CreateClassAsync(AddClassDto newClass, int teacherId);
}