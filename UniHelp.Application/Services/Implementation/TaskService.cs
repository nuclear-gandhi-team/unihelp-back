using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using UniHelp.Domain.Entities;
using UniHelp.Domain.Enums;
using UniHelp.Features.Exceptions;
using UniHelp.Features.TasksFeature.Dtos;
using UniHelp.Services.Interfaces;
using UniHelp.Services.Interfaces.Repositories;
using Task = System.Threading.Tasks.Task;

namespace UniHelp.Services.Implementation;

public class TaskService : ITaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public TaskService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task AddTaskAsync(AddTaskDto addTaskDto)
    {
        if (addTaskDto.DateStart > addTaskDto.DateEnd || addTaskDto.DateStart < DateTime.Now)
        {
            throw new ArgumentException("Wrong date bounds.");
        }
        Domain.Entities.Task task;
        try
        {
            task = _mapper.Map<Domain.Entities.Task>(addTaskDto);
        }
        catch (ArgumentException)
        {
            throw new ArgumentException("Wrong task type");
        }

        var cls = await _unitOfWork.Classes.GetByIdAsync(task.ClassId)
                  ?? throw new ArgumentException($"No Class with id '{task.ClassId}'");
        
        await _unitOfWork.Tasks.AddAsync(task);
        
        if (addTaskDto.TestQuestions is not null)
        {
            var testQuestions = new List<TestQuestion>();

            foreach (var testQuestion in addTaskDto.TestQuestions)
            {
                var tq = new TestQuestion
                {
                    Question = testQuestion.Question,
                    Task = task,
                };

                await _unitOfWork.TestQuestions.AddAsync(tq);

                int counter = 0;
                foreach (var variant in testQuestion.AnswerVariants)
                {
                    var answerVariant = new AnswerVariant
                    {
                        Text = variant,
                        IsCorrect = counter == testQuestion.CorrectAnswer,
                        Question = tq,
                    };

                    counter++;
                    await _unitOfWork.AnswerVariants.AddAsync(answerVariant);
                }
            }

            await _unitOfWork.TestQuestions.AddRangeAsync(testQuestions);
        }

        await _unitOfWork.CommitAsync();
    }

    public async Task SubmitTaskAsync(SubmitTaskDto submitTaskDto, string studentId)
    {
        if (submitTaskDto.File is null || submitTaskDto.File.Length == 0)
        {
            throw new ArgumentException("No file uploaded.");
        }

        using var memoryStream = new MemoryStream();
        await submitTaskDto.File.CopyToAsync(memoryStream);
        
        var user = await _userManager.FindByIdAsync(studentId);
        
        var studentTask = new StudentTask
        {
            StudentId = (int)user!.StudentId!,
            TaskId = submitTaskDto.TaskId,
            HandedDate = DateTime.Now,
            File = memoryStream.ToArray()
        };

        await _unitOfWork.StudentTasks.AddAsync(studentTask);
        await _unitOfWork.CommitAsync();
    }

    public async Task SubmitTestAsync(SubmitTestDto submitTaskDto, string studentId)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(submitTaskDto.TaskId)
            ?? throw new ArgumentException("Wrong task ID");

        int grade = 0;
        
        foreach (var answer in submitTaskDto.Answers)
        {
            if (((await _unitOfWork.AnswerVariants.GetByIdAsync(answer.VariantId))!).IsCorrect)
            {
                grade++;
            }
        }

        grade = task.MaxPoints / submitTaskDto.Answers.Count;

        var user = await _userManager.FindByIdAsync(studentId);
        
        var studentTask = new StudentTask
        {
            StudentId = (int)user!.StudentId!,
            TaskId = submitTaskDto.TaskId,
            HandedDate = DateTime.Now,
            File = null!,
            Grade = grade,
        };

        await _unitOfWork.StudentTasks.AddAsync(studentTask);
        await _unitOfWork.CommitAsync();
    }

    public async Task SetGradeAsync(SetGradeDto setGradeDto)
    {
        var task = await _unitOfWork.Tasks.GetByIdAsync(setGradeDto.TaskId)
                   ?? throw new ArgumentException("No task with Id '{setGradeDto.TaskId}'");

        var studentTask = task.StudentTasks.FirstOrDefault(
            st => st.StudentId == setGradeDto.StudentId && st.TaskId == setGradeDto.TaskId);

        studentTask.Grade = setGradeDto.Grade;

        await _unitOfWork.CommitAsync();
    }

    public async Task<GetTaskDto> GetClosestTaskAsync(int classId)
    {
        var cls = await _unitOfWork.Classes.GetByIdAsync(classId)
            ?? throw new ArgumentException($"No class with Id '{classId}'");

        var closestTask = cls.Tasks.MinBy(t => t.DateEnd);

        return _mapper.Map<GetTaskDto>(closestTask);
    }

    public async Task<IEnumerable<GetTableTaskDto>> GetTasksByClassAndUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        var tasks = user.Student.StudentClasses
            .Select(sc => sc.Class)
            .SelectMany(c => c.Tasks)
            .ToList();
        
        return _mapper.Map<IEnumerable<GetTableTaskDto>>(tasks);
    }

    public async Task<IEnumerable<GetTableTaskDto>> GetTasksByClassAsync(int classId)
    {
        var cls = await _unitOfWork.Classes.GetByIdAsync(classId)
            ?? throw new EntityNotFoundException($"No class with Id '{classId}'");
        return _mapper.Map<IEnumerable<GetTableTaskDto>>(cls.Tasks);
    }
}