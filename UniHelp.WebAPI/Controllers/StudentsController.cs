using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniHelp.Domain.Entities;
using UniHelp.Features.Constants;
using UniHelp.Services.Interfaces;
using UniHelp.WebAPI.Extensions;

namespace UniHelp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;
    private readonly UserManager<User> _userManager;

    public StudentsController(IStudentService studentService, UserManager<User> userManager)
    {
        _studentService = studentService;
        _userManager = userManager;
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
    
    [HttpGet("attendance/{classId}")]
    [Authorize(Roles = UserRoleNames.Student, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> GetStudentAttendanceAsync(int classId)
    {
        var userId = await this.GetUserIdFromJwtAsync();
        if (userId is null)
        {
            return Unauthorized();
        }
        var user = await _userManager.FindByIdAsync(userId);
        var attendance = await _studentService.GetStudentAttendanceAsync(user.Student.Id, classId);
        return Ok(attendance);
    }
}