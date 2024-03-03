using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniHelp.Domain.Entities;
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
    private readonly UserManager<User> _userManager;

    public ClassService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<IEnumerable<GetClassDto>> GetAllTeacherClassesAsync(int teacherId)
    {
        var classes = await _unitOfWork.Classes.GetClassesByTeacherIdAsync(teacherId);
        return _mapper.Map<IEnumerable<GetClassDto>>(classes);
    }

    public async Task<IEnumerable<GetClassDto>> GetClassesAsync()
    {
        var classes = await _unitOfWork.Classes.GetAllAsync();
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
}