using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UniHelp.Domain.Entities;
using UniHelp.Features.ClassFeatures.Dtos;
using UniHelp.Features.Constants;
using UniHelp.Services.Interfaces;
using UniHelp.WebAPI.Extensions;

namespace UniHelp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassesController : ControllerBase
{
    private readonly IClassService _classService;
    private readonly UserManager<User> _userManager;

    public ClassesController(IClassService classService, UserManager<User> userManager)
    {
        _classService = classService;
        _userManager = userManager;
    }

    [HttpGet("get-classes")]
    public async Task<IActionResult> GetAllClassesOfTeacher()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _userManager.FindByIdAsync(userId);

        if (user is not null && await _userManager.IsInRoleAsync(user, UserRoleNames.Teacher))
        {
            var classes = await _classService.GetClassesAsync((int)user.TeacherId);
            return Ok(classes);
        }
        else
        {
            var classes = await _classService.GetClassesAsync();
            return Ok(classes);
        }
    }

    [HttpGet("get-class/{id}")]
    public async Task<IActionResult> GetClassById(int id)
    {
        var classEntity = await _classService.GetClassByIdAsync(id);
        return Ok(classEntity);
    }

    [HttpPost("create-class")]
    [Authorize(Roles = UserRoleNames.Student, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> CreateClass([FromBody] AddClassDto newClass)
    {
        var classEntity = await _classService.CreateClassAsync(newClass, await this.GetUserIdFromJwtAsync());
        return Ok(classEntity);
    }

    [HttpPost("add-student-to-class")]
    [Authorize(Roles = UserRoleNames.Student, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> AddStudentToClass([FromBody] AddStudentToClassDto addStudentToClassDto)
    {
        var classEntity =
            await _classService.AddStudentToClassAsync(addStudentToClassDto.ClassId, addStudentToClassDto.StudentId);
        return Ok(classEntity);
    }
}