using AutoMapper;
using UniHelp.Domain.Enums;
using UniHelp.Features.Exceptions;
using UniHelp.Features.StudentFeatures.Dtos;
using UniHelp.Services.Interfaces;
using UniHelp.Services.Interfaces.Repositories;

namespace UniHelp.Services.Implementation;

public class StudentService : IStudentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GetOneDto> GetStudentByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid id");
        }
        var student = await _unitOfWork.Students.GetStudentByIdWithUserAsync(id);
        return _mapper.Map<GetOneDto>(student);
    }

    public async Task<IEnumerable<GetAllDto>> GetAllStudentsAsync()
    {
        var students = await _unitOfWork.Students.GetAllStudentsWithUserAsync();
        return _mapper.Map<IEnumerable<GetAllDto>>(students);
    }

    public async Task<IEnumerable<GetAllDto>> GetStudentsByClassAsync(int classId)
    {
        if (classId <= 0)
        {
            throw new ArgumentException("Invalid class id");
        }
        var students = await _unitOfWork.Students.GetStudentsByClassIdAsync(classId);
        var studentDto = _mapper.Map<IEnumerable<GetAllDto>>(students);
        return studentDto;
    }
    
    public async Task<double> GetStudentAttendanceAsync(int studentId, int classId)
    {
        var studentClass = await _unitOfWork.StudentClasses.GetStudentClassAsync(studentId, classId);
        if (studentClass == null)
        {
            throw new EntityNotFoundException("Student not found in class");
        }

        var totalClasses = studentClass.Class.ClassesNumber;
        var attended = 0;
        for (int i = 0; i < studentClass.Class.Tasks.Count(t => t.Type == TaskType.Class); i++)
        {
            var task = studentClass.Class.Tasks.Where(t => t.Type == TaskType.Class).ElementAt(i);
            if(task.StudentTasks != null) 
            {
                var studentTask = task.StudentTasks.FirstOrDefault(st => st.StudentId == studentId);
                if (studentTask != null && studentTask.Grade > 0)
                {
                    attended++;
                }
            }
        }
        return ((double)attended / totalClasses) * 100;
    }
}