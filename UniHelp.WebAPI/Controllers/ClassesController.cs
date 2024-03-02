using Microsoft.AspNetCore.Mvc;
using UniHelp.Features.ClassFeatures.Dtos;
using UniHelp.Services.Interfaces;

namespace UniHelp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassesController : ControllerBase
{
    private readonly IClassService _classService;

    public ClassesController(IClassService classService)
    {
        _classService = classService;
    }
    
    [HttpGet("get-classes")]
    public async Task<IActionResult> GetAllClassesOfTeacher(int teacherId)
    {
        var classes = await _classService.GetAllTeacherClassesAsync(teacherId);
        return Ok(classes);
    }
    
    [HttpGet("get-class/{id}")]
    public async Task<IActionResult> GetClassById(int id)
    {
        var classEntity = await _classService.GetClassByIdAsync(id);
        return Ok(classEntity);
    }
    
    [HttpPost("create-class")]
    public async Task<IActionResult> CreateClass([FromBody] AddClassDto newClass)
    {
        var teacherId = 2; // TODO: get teacher id from token
        var classEntity = await _classService.CreateClassAsync(newClass, teacherId);
        return Ok(classEntity);
    }
    
    
    //TODO: add authorization
    [HttpPost("add-student-to-class")]
    public async Task<IActionResult> AddStudentToClass([FromBody] AddStudentToClassDto addStudentToClassDto)
    {
        var classEntity = await _classService.AddStudentToClassAsync(addStudentToClassDto.ClassId, addStudentToClassDto.Email);
        return Ok(classEntity);
    }
}