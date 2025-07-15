using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserPortal.Application.Common.Interfaces;
using UserPortal.Application.DTOs.Request;
using UserPortal.Application.DTOs.Response;
using UserPortal.Application.User.Commands.UpdateUser;
using UserPortal.Domain.Entities;

namespace UserPortal.Application.UnitTest.Commands
{
    [TestFixture]
    public class UpdateUserCommandHandlerTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private UpdateUserCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _handler = new UpdateUserCommandHandler(
                _userRepositoryMock.Object,
                _mapperMock.Object,
                _unitOfWorkMock.Object
            );
        }

        [Test]
        public async Task Handle_ShouldUpdateUserAndReturnDto_WhenUserExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var requestDto = new UpdateUserRequestDto ( FirstName : "Mrudush", LastName : "Cholliyil", Email : "mrudush@gmail.com", Phone : "1234567890" );
            var command = new UpdateUserCommand (UserId : userId, Request : requestDto);
            var userEntity = new UserEntity { FirstName = "Mrudush", LastName = "Cholliyil", Email = "mrudush@gmail.com", Phone = "1234567890" };
            var updatedUserEntity = new UserEntity { FirstName = "Mrudush", LastName = "Cholliyil", Email = "mrudush@gmail.com", Phone = "1234567890" };
            var userResponseDto = new UserResponseDto (Id : userId, FirstName : "Mrudush", LastName : "Cholliyil", Email : "mrudush@gmail.com", Phone : "1234567890");

            _mapperMock.Setup(m => m.Map<UserEntity>(requestDto)).Returns(userEntity);
            _userRepositoryMock.Setup(r => r.UpdateAsync(userId, userEntity)).ReturnsAsync(updatedUserEntity);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
            _mapperMock.Setup(m => m.Map<UserResponseDto>(updatedUserEntity)).Returns(userResponseDto);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(userResponseDto);
            _userRepositoryMock.Verify(r => r.UpdateAsync(userId, userEntity), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_ShouldThrowInvalidOperationException_WhenUserNotFound()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var requestDto = new UpdateUserRequestDto(FirstName: "Mrudush", LastName: "Cholliyil", Email: "mrudush@gmail.com", Phone: "1234567890");
            var command = new UpdateUserCommand(UserId: userId, Request: requestDto);
            var userEntity = new UserEntity { FirstName = "Mrudush", LastName = "Cholliyil", Email = "mrudush@gmail.com", Phone = "1234567890" };

            _mapperMock.Setup(m => m.Map<UserEntity>(requestDto)).Returns(userEntity);
            _userRepositoryMock.Setup(r => r.UpdateAsync(userId, userEntity)).ReturnsAsync((UserEntity)null);

            // Act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage($"User with ID {userId} not found or update failed.");
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
    }
}
