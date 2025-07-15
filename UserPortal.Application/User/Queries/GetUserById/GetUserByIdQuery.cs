using MediatR;
using UserPortal.Application.DTOs.Response;

namespace UserPortal.Application.User.Queries.GetUserById
{
    /// <summary>
    /// Query for retrieving a user by their unique identifier.
    /// </summary>
    /// <param name="UserId">User Id</param>
    public record GetUserByIdQuery(Guid UserId) : IRequest<UserResponseDto>;

}
