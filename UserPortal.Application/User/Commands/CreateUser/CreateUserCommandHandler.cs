using AutoMapper;
using MediatR;
using UserPortal.Application.Common.Interfaces;
using UserPortal.Application.DTOs.Response;
using UserPortal.Domain.Entities;

namespace UserPortal.Application.User.Commands.CreateUser
{
    /// <summary>
    /// Handler for the CreateUserCommand, which creates a new user in the system.
    /// </summary>
    /// <param name="UserRepository">User Repository</param>
    /// <param name="Mapper">Automapper</param>
    /// <param name="UnitOfWork">UnitOfWork</param>
    /// <param name="Logger">Logger</param>
    public class CreateUserCommandHandler(IUserRepository UserRepository, 
        IMapper Mapper, 
        IUnitOfWork UnitOfWork, 
        ILoggerService<CreateUserCommandHandler> Logger) :
        IRequestHandler<CreateUserCommand, UserResponseDto>
    {
        public async Task<UserResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            Logger.LogInfo($"Handling CreateUserCommand for user: {request.Request.FirstName} {request.Request.LastName}");
            
            var userEntity = Mapper.Map<UserEntity>(request.Request);
            var createdUser = await UserRepository.CreateAsync(userEntity);

            if (createdUser is null)
            {
                throw new InvalidOperationException("User creation failed. The repository returned no value.");
            }

            await UnitOfWork.SaveChangesAsync();
            return Mapper.Map<UserResponseDto>(createdUser);
        }
    }
}
