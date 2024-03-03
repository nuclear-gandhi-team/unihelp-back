using UniHelp.Domain.Entities;
using UniHelp.Features.UserFeatures.Dtos;
using Task = System.Threading.Tasks.Task;

namespace UniHelp.Services.Interfaces;

public interface IUserService
{
    Task<LoginResponseDto> LoginUserAsync(LoginUserDto loginUserDto);
    
    Task RegisterTeacherUserAsync(RegisterUserDto registerUserDto);
    
    Task RegisterStudentUserAsync(RegisterStudentDto registerUserDto);

    Task<GetFullUserDto> GetUserByIdAsync(string id);

    Task UpdateUserDataAsync(UpdateUserDto updateUserDto, string userId);

    Task UpdateUserPasswordAsync(UpdateUserPasswordDto updateUserDto, string userId);

}