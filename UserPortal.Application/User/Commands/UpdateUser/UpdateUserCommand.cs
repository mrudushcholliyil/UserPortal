using MediatR;
using UserPortal.Application.DTOs.Request;
using UserPortal.Application.DTOs.Response;

namespace UserPortal.Application.User.Commands.UpdateUser
{
    /// <summary>
    /// Command for updating an existing user.
    /// </summary>
    /// <param name="UserId">user id</param>
    /// <param name="Request">UpdateUserRequestDto</param>
    public record UpdateUserCommand(Guid UserId, UpdateUserRequestDto Request) : IRequest<UserResponseDto>;
}
