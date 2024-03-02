using Microsoft.AspNetCore.Mvc;
using UniHelp.Services.Interfaces;

namespace UniHelp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllStudentsAsync()
    {
        var students = await _studentService.GetAllStudentsAsync();
        return Ok(students);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentByIdAsync(int id)
    {
        var student = await _studentService.GetStudentByIdAsync(id);
        return Ok(student);
    }
    
    [HttpGet("class/{classId}")]
    public async Task<IActionResult> GetStudentsByClassAsync(int classId)
    {
        var students = await _studentService.GetStudentsByClassAsync(classId);
        return Ok(students);
    }
}