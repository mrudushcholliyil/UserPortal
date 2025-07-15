using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using UserPortal.Application.Common.Interfaces;
using UserPortal.Application.DTOs.Response;
using UserPortal.Application.User.Commands.CreateUser;

namespace UserPortal.Application.User.Queries.GetUserById
{
    /// <summary>
    /// Handler for the GetUserByIdQuery, which retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="UserRepository">UserRepository</param>
    /// <param name="Mapper">Mapper</param>
    public class GetUserByIdQueryHandler(IUserRepository UserRepository, 
        IMapper Mapper,
        ILoggerService<CreateUserCommandHandler> Logger) :
        IRequestHandler<GetUserByIdQuery, UserResponseDto>
    {
        public async Task<UserResponseDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            Logger.LogInfo($"Handling GetUserByIdQuery for user ID: {request.UserId}");

            var user = await UserRepository.GetByIdAsync(request.UserId);

            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {request.UserId} was not found.");
            }

            return Mapper.Map<UserResponseDto>(user);
        }
    }
}
