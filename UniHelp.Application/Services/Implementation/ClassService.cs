using AutoMapper;
using UniHelp.Domain.Entities;
using UniHelp.Features.ClassFeatures.Dtos;
using UniHelp.Features.Exceptions;
using UniHelp.Services.Interfaces;
using UniHelp.Services.Interfaces.Repositories;

namespace UniHelp.Services.Implementation;

public class ClassService : IClassService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ClassService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetClassDto>> GetClassesAsync(int teacherId)
    {
        var classes = await _unitOfWork.Classes.GetClassesByTeacherIdAsync(teacherId);
        return _mapper.Map<IEnumerable<GetClassDto>>(classes);
    }

    public async Task<GetClassDto> GetClassByIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Invalid id");
        }
        var classEntity = await _unitOfWork.Classes.GetByIdAsync(id);
        if (classEntity == null)
        {
            throw new EntityNotFoundException("Class not found");
        }
        return _mapper.Map<GetClassDto>(classEntity);
    }

    public async Task<GetClassDto> CreateClassAsync(AddClassDto newClass, int teacherId)
    {
        var classEntity = _mapper.Map<Class>(newClass) 
                          ?? throw new ArgumentException("Error mapping class while creating");
        
        classEntity.TeacherId = teacherId;
        await _unitOfWork.Classes.AddAsync(classEntity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<GetClassDto>(classEntity);
    }

    public async Task<GetClassDto> AddStudentToClassAsync(int classId, int studentId)
    {
        if (classId <= 0 || studentId <= 0)
        {
            throw new ArgumentException("Invalid id");
        }
        var classEntity = await _unitOfWork.Classes.GetClassWithStudentsAsync(classId);
        if (classEntity == null)
        {
            throw new EntityNotFoundException("Class not found");
        }
        if (classEntity.StudentClasses.Any(sc => sc.StudentId == studentId))
        {
            throw new StudentAlreadyInClassException("Student already in class");
        }

        var studentEntity = await _unitOfWork.Students.GetStudentWithClassesAsync(studentId);
        if (studentEntity == null)
        {
            throw new EntityNotFoundException("Student not found");
        }
        classEntity.StudentClasses.Add(new StudentClass
        {
            ClassId = classId,
            StudentId = studentId
        });
        
        await _unitOfWork.CommitAsync();
        return _mapper.Map<GetClassDto>(classEntity);
    }
}