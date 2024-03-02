using AutoMapper;
using Microsoft.AspNetCore.Http;
using UniHelp.Domain.Entities;
using UniHelp.Features.TasksFeature.Dtos;
using UniHelp.Services.Interfaces;
using UniHelp.Services.Interfaces.Repositories;
using Task = System.Threading.Tasks.Task;

namespace UniHelp.Services.Implementation;

public class TaskService : ITaskService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TaskService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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
                foreach (var variant in testQuestion.AnswerVariants)
                {
                    var answerVariant = new AnswerVariant
                    {
                        Text = variant,
                        IsCorrect = false,
                        Question = tq,
                    };

                    await _unitOfWork.AnswerVariants.AddAsync(answerVariant);
                }
                
                await _unitOfWork.AnswerVariants.AddAsync(new AnswerVariant
                {
                    Text = testQuestion.CorrectAnswer,
                    IsCorrect = true,
                    Question = tq,
                });
            }

            await _unitOfWork.TestQuestions.AddRangeAsync(testQuestions);
        }

        await _unitOfWork.CommitAsync();
    }

    public async Task SubmitTaskAsync(SubmitTaskDto submitTaskDto, IFormFile file, string studentId)
    {
        if (file is null || file.Length == 0)
        {
            throw new ArgumentException("No file uploaded.");
        }

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        var studentTask = new StudentTask
        {
            StudentId = studentId,
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
        
        /*foreach (var question in task.TestQuestions)
        {
            if(question.AnswerVariants.FirstOrDefault(av => av.IsCorrect) == submitTaskDto.Answers[])
        }*/
        var studentTask = new StudentTask
        {
            StudentId = studentId,
            TaskId = submitTaskDto.TaskId,
            HandedDate = DateTime.Now,
            File = null!,
            Grade = grade,
        };

        await _unitOfWork.StudentTasks.AddAsync(studentTask);
        await _unitOfWork.CommitAsync();
    }
}