using UniHelp.Domain.Entities;
using UniHelp.Features.ClassFeatures.Dtos;
using UniHelp.Features.StudentFeatures.Dtos;

namespace UniHelp.Services.Interfaces;

public interface IStudentService
{
    Task<GetOneDto> GetStudentByIdAsync(int id);
    
    Task<IEnumerable<GetAllDto>> GetAllStudentsAsync();
    
    Task<IEnumerable<GetAllDto>> GetStudentsByClassAsync(int classId);
    
    Task<double> GetStudentAttendanceAsync(int studentId, int classId);
}