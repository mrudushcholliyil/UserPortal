using MediatR;
using UserPortal.Application.DTOs.Request;
using UserPortal.Application.DTOs.Response;

namespace UserPortal.Application.User.Commands.CreateUser
{
    /// <summary>
    /// Command to create a new user,Input is CreateUserRequestDto and return is UserResponseDto.
    /// </summary>
    /// <param name="Request">Request</param>
    public record CreateUserCommand(CreateUserRequestDto Request) : IRequest<UserResponseDto>;
    
}
