using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UniHelp.Features.UserFeatures.Dtos;
using UniHelp.Services.Interfaces;
using UniHelp.WebAPI.Extensions;

namespace UniHelp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route("register-teacher")]
    public async Task<IActionResult> RegisterTeacher([FromBody] RegisterUserDto registerUserDto)
    {
        await _userService.RegisterTeacherUserAsync(registerUserDto);

        return Ok();
    }
    
    [HttpPost]
    [Route("register-student")]
    public async Task<IActionResult> RegisterStudent([FromBody] RegisterStudentDto registerUserDto)
    {
        await _userService.RegisterStudentUserAsync(registerUserDto);

        return Ok();
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> RegisterStudent([FromBody] LoginUserDto loginUserDto)
    {
        var dto = await _userService.LoginUserAsync(loginUserDto);

        return Ok(dto);
    }
    
    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("update-name")]
    public async Task<IActionResult> UpdateUserData([FromBody] UpdateUserDto updateUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        await _userService.UpdateUserDataAsync(updateUserDto, (await this.GetUserIdFromJwtAsync())!);

        return Ok();
    }
    
    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("update-password")]
    public async Task<IActionResult> UpdateUserPassword([FromBody] UpdateUserPasswordDto updateUserDto)
    {
        await _userService.UpdateUserPasswordAsync(updateUserDto, (await this.GetUserIdFromJwtAsync())!);

        return Ok();
    }
}