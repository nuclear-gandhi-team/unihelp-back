using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UniHelp.Domain.Entities;
using UniHelp.Features.ClassFeatures.Dtos;
using UniHelp.Features.StudentFeatures.Dtos;
using UniHelp.Services.Interfaces;
using UniHelp.Services.Interfaces.Repositories;

namespace UniHelp.Services.Implementation;

public class StudentService : IStudentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public StudentService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
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

    public async Task<IEnumerable<GetClassDto>> GetStudentClassesAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        var classes = user.Student.StudentClasses
            .Select(sc => sc.Class)
            .ToList();
        
        return _mapper.Map<IEnumerable<GetClassDto>>(classes);
    }
}