using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniHelp.Domain.Entities;
using UniHelp.Domain.Enums;
using UniHelp.Features.ClassFeatures.Dtos;
using UniHelp.Features.Exceptions;
using UniHelp.Services.Interfaces;
using UniHelp.Services.Interfaces.Repositories;

namespace UniHelp.Services.Implementation;

public class ClassService : IClassService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public ClassService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<IEnumerable<GetBriefClassDto>> GetClassesAsync(int teacherId)
    {
        var classes = await _unitOfWork.Classes.GetClassesByTeacherIdAsync(teacherId);
        var dtos = _mapper.Map<IEnumerable<GetBriefClassDto>>(classes);
        foreach (var dto in dtos)
        {
            dto.StudentsCount = await GetStudentsOnClassCountAsync(dto.ClassId);
        }

        return dtos;
    }

    public async Task<IEnumerable<GetBriefClassDto>> GetClassesAsync()
    {
        var classes = await _unitOfWork.Classes.GetAllAsync();
        return _mapper.Map<IEnumerable<GetBriefClassDto>>(classes);
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

    public async Task<GetClassDto> CreateClassAsync(AddClassDto newClass, string userId)
    {
        var classEntity = _mapper.Map<Class>(newClass) 
                          ?? throw new ArgumentException("Error mapping class while creating");

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId)
            ?? throw new ArgumentException($"No user with Id '{userId}'");
        
        classEntity.TeacherId = (int)user.TeacherId!;
        await _unitOfWork.Classes.AddAsync(classEntity);
        await _unitOfWork.CommitAsync();
        return _mapper.Map<GetClassDto>(classEntity);
    }

    public async Task<GetClassDto> AddStudentToClassAsync(int classId, string email)
    {
        if (classId <= 0 || string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("Invalid id or email");
        }
        var classEntity = await _unitOfWork.Classes.GetClassWithStudentsAsync(classId);
        if (classEntity == null)
        {
            throw new EntityNotFoundException("Class not found");
        }

        var userEntity = await _userManager.Users
            .Include(u=>u.Student)
            .FirstOrDefaultAsync(u => u.Email == email);
        
        var studentEntity = userEntity.Student;
        if (studentEntity == null)
        {
            throw new EntityNotFoundException("Student not found");
        }
        if (classEntity.StudentClasses.Any(sc => sc.StudentId == studentEntity.Id))
        {
            throw new StudentAlreadyInClassException("Student already in class");
        }

        classEntity.StudentClasses.Add(new StudentClass
        {
            ClassId = classId,
            StudentId = studentEntity.Id
        });
        
        await _unitOfWork.CommitAsync();
        return _mapper.Map<GetClassDto>(classEntity);
    }

    public Task<int> GetStudentsOnClassCountAsync(int classId)
    {
        return _unitOfWork.Classes.GetStudentsOnClassCountAsync(classId);
    }
}