using System.Globalization;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UniHelp.Domain.Entities;
using UniHelp.Features.ClassFeatures.Dtos;
using UniHelp.Domain.Enums;
using UniHelp.Features.Exceptions;
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
        
        var dtos = _mapper.Map<IEnumerable<GetClassDto>>(classes);

        foreach (var dto in dtos)
        {
            dto.TeacherName = await _unitOfWork.Classes.GetFullTeacherNameByClassAsync(dto.ClassId);
        }

        return dtos;
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

    public async Task<bool> CheckIfStudentAttendedAsync(int studentId, int taskId)
    {
        var studentTask = await _unitOfWork.StudentTasks.GetStudentTaskByIdAsync(studentId, taskId);
        if (studentTask == null)
        {
            throw new EntityNotFoundException("Student task not found");
        }
        if (studentTask.Task.Type != TaskType.Class)
        {
            throw new ArgumentException("Task is not a class");
        }
        if (studentTask.Grade > 0)
        {
            return true;
        }
        return false;
    }

    public async Task<IEnumerable<GetGradeByMonthsDto>> GetStudentAvgGradeByMonthsAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null || user.Student == null)
        {
            throw new ArgumentException("User or student not found.");
        }

        var tasks = user.Student.StudentTasks
            .Select(st => st.Task)
            .ToList();

        var gradeByMonthsDtos = new List<GetGradeByMonthsDto>();

        const int monthsCount = 12;
        for (int i = 1; i <= monthsCount; i++)
        {
            var monthlyTasks = tasks.Where(t => t.DateEnd.Month == i).ToList();

            if (monthlyTasks.Any())
            {
                var averageGrade = monthlyTasks
                    .Select(t =>
                    {
                        var st = t.StudentTasks.FirstOrDefault(st => st.StudentId == user.Student.Id);
                        return st?.Grade ?? 0;
                    })
                    .Average();

                gradeByMonthsDtos.Add(new GetGradeByMonthsDto
                {
                    Month = new DateTime(1, i, 1).ToString("MMM", CultureInfo.InvariantCulture),
                    Total = averageGrade,
                });
            }
            else
            {
                gradeByMonthsDtos.Add(new GetGradeByMonthsDto
                {
                    Month = new DateTime(1, i, 1).ToString("MMM", CultureInfo.InvariantCulture),
                    Total = 0,
                });
            }
        }

        return gradeByMonthsDtos;
    }
}