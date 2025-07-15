using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserPortal.Application.Common.Exceptions;
using UserPortal.Application.DTOs.Request;
using UserPortal.Application.User.Commands.CreateUser;
using UserPortal.Application.User.Commands.UpdateUser;
using UserPortal.Application.User.Queries.GetUserById;

namespace UserPortal.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IMediator Mediator) : ControllerBase
    {
        /// <summary>
        /// Adds a new user to the system asynchronously.
        /// </summary>
        /// <param name="userDto">The data transfer object containing the details of the user to be created. Cannot be null.</param>
        /// <returns>
        /// Returns <see cref="BadRequestObjectResult"/> if the provided <paramref name="userDto"/> is null.
        /// Returns <see cref="OkObjectResult"/> with the result of the user creation operation otherwise.
        /// </returns>
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUserAsync([FromBody] CreateUserRequestDto userDto)
        {
            if (userDto is null)
            {
                return BadRequest("User data is required.");
            }
            var result = await Mediator.Send(new CreateUserCommand(userDto));

            return Ok(result);
        }

        /// <summary>
        /// Retrieves a user by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to retrieve.</param>
        /// <returns>
        /// Returns <see cref="NotFoundResult"/> if the user does not exist.
        /// Returns <see cref="OkObjectResult"/> with the user data if found.
        /// </returns>
        [HttpGet("GetUser/{userId}")]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] Guid userId)
        {
            if (userId == Guid.Empty)
            {
                throw new BadRequestException("User ID is required.");
            }

            var result = await Mediator.Send(new GetUserByIdQuery(userId));

            if (result is null)
            {
                throw new NotFoundException($"User with ID {userId} not found.");
            }

            if (result == null)
            {
                throw new NotFoundException($"User with ID {userId} not found.");
            }

            return Ok(result);
        }

        /// <summary>
        /// Updates an existing user's information by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to update.</param>
        /// <param name="updateUserRequestDto">The data transfer object containing updated user details. Cannot be null.</param>
        /// <returns>
        /// Returns <see cref="BadRequestObjectResult"/> if the userId is empty or the request DTO is null.
        /// Returns <see cref="NotFoundResult"/> if the user does not exist or the update fails.
        /// Returns <see cref="OkObjectResult"/> with the result of the update operation if successful.
        /// </returns>
        [HttpPut("UpdateUser/{userId}")]
        public async Task<IActionResult> UpdateUserByIdAsync([FromRoute] Guid userId,
            [FromBody] UpdateUserRequestDto updateUserRequestDto)
        {
            if (userId == Guid.Empty)
            {
                throw new BadRequestException("User ID is required.");
            }

            if (updateUserRequestDto is null)
            {
                throw new BadRequestException("User data is required.");
            }

            var result = await Mediator.Send(new UpdateUserCommand(userId, updateUserRequestDto));

            if (result == null)
            {
                throw new NotFoundException($"User with ID {userId} not found or update failed.");
            }

            return Ok(result);
        }
    }
}

