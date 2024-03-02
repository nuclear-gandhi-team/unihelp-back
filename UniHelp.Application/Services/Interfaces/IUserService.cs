using UniHelp.Features.UserFeatures.Dtos;

namespace UniHelp.Services.Interfaces;

public interface IUserService
{
    Task<LoginResponseDto> LoginUserAsync(LoginUserDto loginUserDto);
    
    Task RegisterUserAsync(RegisterUserDto registerUserDto);

    Task<GetFullUserDto> GetUserByIdAsync(string id);

    Task UpdateUserDataAsync(UpdateUserDto updateUserDto);

    Task UpdateUserPasswordAsync(UpdateUserPasswordDto updateUserDto);

}