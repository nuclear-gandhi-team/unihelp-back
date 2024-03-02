using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UniHelp.Domain.Entities;
using UniHelp.Features.Constants;
using UniHelp.Features.Exceptions;
using UniHelp.Features.Time;
using UniHelp.Features.UserFeatures.Dtos;
using UniHelp.Features.UserFeatures.Options;
using UniHelp.Services.Interfaces;
using UniHelp.Services.Interfaces.Repositories;
using Task = System.Threading.Tasks.Task;

namespace UniHelp.Services.Implementation;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITimeProvider _timeProvider;

    private readonly JwtOptions _jwtOptions;
    
    public UserService(
        UserManager<User> userManager,
        IMapper mapper,
        IOptions<JwtOptions> jwtOptions,
        IUnitOfWork unitOfWork,
        ITimeProvider timeProvider)
    {
        _userManager = userManager;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _timeProvider = timeProvider;
        _jwtOptions = jwtOptions.Value;
    }
    
    public async Task<LoginResponseDto> LoginUserAsync(LoginUserDto loginUserDto)
    {
        var userExists = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == loginUserDto.Email)
                         ?? throw new ArgumentException("User doest exists");

        return await _userManager.CheckPasswordAsync(userExists, loginUserDto.Password)
            ? new LoginResponseDto 
            { 
                Token =  GenerateJwtAsync(userExists),
                Role = await _userManager.IsInRoleAsync(userExists, UserRoleNames.Student) 
                    ? UserRoleNames.Student
                    : UserRoleNames.Teacher 
            }
            : throw new AuthenticationException("Wrong password");
    }

    public async Task RegisterTeacherUserAsync(RegisterUserDto registerUserDto)
    {
        var user = await RegisterUserAsync(registerUserDto);

        await _userManager.AddToRoleAsync(user, UserRoleNames.Teacher);
        
        var teacher = new Teacher { User = user};

        await _unitOfWork.Teachers.AddAsync(teacher);
    }

    public async Task RegisterStudentUserAsync(RegisterStudentDto registerUserDto)
    {
        var user = await RegisterUserAsync(registerUserDto);

        await _userManager.AddToRoleAsync(user, UserRoleNames.Student);
        
        var student = _mapper.Map<Student>(registerUserDto);
        student.User = user;

        await _unitOfWork.Students.AddAsync(student);
    }

    public async Task<GetFullUserDto> GetUserByIdAsync(string id)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);

        return _mapper.Map<GetFullUserDto>(user)!;
    }

    public async Task UpdateUserDataAsync(UpdateUserDto updateUserDto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == updateUserDto.Id)
                   ?? throw new EntityNotFoundException($"No user with Id '{updateUserDto.Id}'");

        if (updateUserDto.NewFirstName is not null)
        {
            user.FirstName = updateUserDto.NewFirstName;
        }

        if (updateUserDto.NewLastName is not null)
        {
            user.LastName = updateUserDto.NewLastName;
        }
        
        user.UserName = updateUserDto.NewFirstName + " " + updateUserDto.NewLastName;
        user.NormalizedUserName =updateUserDto.NewFirstName + " " + updateUserDto.NewLastName;

        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
        {
            throw new UserUpdateException($"Error updating user with Id '{user.Id}'");
        }
    }

    public async Task UpdateUserPasswordAsync(UpdateUserPasswordDto updateUserDto)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == updateUserDto.Id)
                   ?? throw new EntityNotFoundException($"No user with Id '{updateUserDto.Id}'");

        if (!await _userManager.CheckPasswordAsync(user, updateUserDto.OldPassword))
        {
            throw new UserUpdateException("Wrong password.");
        }

        if (updateUserDto.NewPassword != updateUserDto.ConfirmNewPassword)
        {
            throw new UserUpdateException("Passwords are not equal.");
        }
        
        var removePasswordResult = await _userManager.RemovePasswordAsync(user);
        if (!removePasswordResult.Succeeded)
        {
            throw new UserUpdateException($"Error deleting password for user Id '{user.Id}'");
        }

        var addPasswordResult = await _userManager.AddPasswordAsync(user, updateUserDto.NewPassword);
        if (!addPasswordResult.Succeeded)
        {
            throw new UserUpdateException($"Error adding new password for user Id '{user.Id}'");
        }
    }
    
    private string GenerateJwtAsync(User user)
    {
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName ?? string.Empty),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(JwtRegisteredClaimNames.Sub, user.UserName ?? string.Empty),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Secret));

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            expires: DateTime.UtcNow.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        return $"Bearer {tokenValue}";
    }
    
    private async Task<User> RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        if (registerUserDto.Password != registerUserDto.ConfirmPassword)
        {
            throw new ArgumentException("Password is not equal to Confirm Password");
        }
        var userExists = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == registerUserDto.Email);

        if (userExists is not null)
        {
            throw new ArgumentException("User with this Name already exists.");
        }

        var newUser = _mapper.Map<User>(registerUserDto)
                      ?? throw new ArgumentException("Invalid data.");
        
        var result = await _userManager.CreateAsync(newUser, registerUserDto.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors
                .Aggregate(string.Empty, (current, err) => current + err.Description);

            throw new ArgumentException($"User cannot be created: {errors}");
        }

        return newUser;
    }
}