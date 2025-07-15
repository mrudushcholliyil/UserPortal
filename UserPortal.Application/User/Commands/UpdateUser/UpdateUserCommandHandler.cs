using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using UserPortal.Application.Common.Interfaces;
using UserPortal.Application.DTOs.Response;
using UserPortal.Application.User.Commands.CreateUser;
using UserPortal.Domain.Entities;

namespace UserPortal.Application.User.Commands.UpdateUser
{
    /// <summary>
    /// Handler for the UpdateUserCommand, which updates an existing user in the system.
    /// </summary>
    /// <param name="UserRepository">UserRepository</param>
    /// <param name="Mapper">Mapper</param>
    /// <param name="UnitOfWork">UnitOfWork</param>
    public class UpdateUserCommandHandler(IUserRepository UserRepository,
        IMapper Mapper, 
        IUnitOfWork UnitOfWork,
        ILoggerService<CreateUserCommandHandler> Logger) :
        IRequestHandler<UpdateUserCommand, UserResponseDto>
    {
        public async Task<UserResponseDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            Logger.LogInfo($"Handling UpdateUserCommand for user ID: {request.UserId}");

            var userEntity = Mapper.Map<UserEntity>(request.Request);
            var updatedUser = await UserRepository.UpdateAsync(request.UserId, userEntity);

            if (updatedUser == null)
            {
                throw new InvalidOperationException($"User with ID {request.UserId} not found or update failed.");
            }

            await UnitOfWork.SaveChangesAsync();

            return Mapper.Map<UserResponseDto>(updatedUser);
        }
    }
}
