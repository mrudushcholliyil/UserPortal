

using AutoMapper;
using FluentAssertions;
using Moq;
using System.Numerics;
using UserPortal.Application.Common.Interfaces;
using UserPortal.Application.DTOs.Request;
using UserPortal.Application.DTOs.Response;
using UserPortal.Application.User.Commands.CreateUser;
using UserPortal.Domain.Entities;

namespace UserPortal.Application.UnitTest.Commands
{
    [TestFixture]
    public class CreateUserCommandHandlerTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<ILoggerService<CreateUserCommandHandler>> _loggerMock;

        private CreateUserCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _loggerMock = new Mock<ILoggerService<CreateUserCommandHandler>>();

            _handler = new CreateUserCommandHandler(
                _userRepositoryMock.Object,
                _mapperMock.Object,
                _unitOfWorkMock.Object,
                _loggerMock.Object
            );
        }

        [Test]
        public async Task Handle_ShouldReturnUserResponseDto_WhenUserIsCreated()
        {
            // Arrange
            var requestDto = new CreateUserRequestDto(
                FirstName: "Mrudush",
                LastName: "Cholliyil",
                Email: "Mrudushc@gmail.com",
                Phone: "12345678"
            );

            var command = new CreateUserCommand(requestDto);

            var userEntity = new UserEntity
            {
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                Email = requestDto.Email,
                Phone = requestDto.Phone
            };

            Guid newGuid = Guid.NewGuid();

            var createdUserEntity = new UserEntity
            {
                Id = newGuid,
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                Email = requestDto.Email,
                Phone = requestDto.Phone
            };

            var userResponseDto = new UserResponseDto
            (
                Id: newGuid,
                FirstName: requestDto.FirstName,
                LastName: requestDto.LastName,
                Email: requestDto.Email,
                Phone: requestDto.Phone
            );

            _mapperMock.Setup(m => m.Map<UserEntity>(requestDto)).Returns(userEntity);
            _userRepositoryMock.Setup(r => r.CreateAsync(userEntity))
                .ReturnsAsync(createdUserEntity);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
            _mapperMock.Setup(m => m.Map<UserResponseDto>(createdUserEntity)).Returns(userResponseDto);


            // Act

            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(userResponseDto);
            _mapperMock.Verify(m => m.Map<UserEntity>(requestDto), Times.Once);
            _userRepositoryMock.Verify(r => r.CreateAsync(userEntity), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
            _mapperMock.Verify(m => m.Map<UserResponseDto>(createdUserEntity), Times.Once);

        }

        [Test]
        public void Handle_ShouldThrowInvalidOperationException_WhenRepositoryReturnsNull()
        {
            // Arrange
            var requestDto = new CreateUserRequestDto(
                FirstName: "Mrudush",
                LastName: "Cholliyil",
                Email: "Mrudushc@gmail.com",
                Phone: "12345678"
            );

            var command = new CreateUserCommand(requestDto);

            var userEntity = new UserEntity
            {
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                Email = requestDto.Email,
                Phone = requestDto.Phone
            };

            _mapperMock.Setup(m => m.Map<UserEntity>(requestDto)).Returns(userEntity);
            _userRepositoryMock.Setup(r => r.CreateAsync(userEntity)).ReturnsAsync((UserEntity)null);

            // Act  Assert
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
            act.Should().ThrowAsync<InvalidOperationException>();

            _mapperMock.Verify(m => m.Map<UserEntity>(requestDto), Times.Once);
            _userRepositoryMock.Verify(r => r.CreateAsync(userEntity), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
            _mapperMock.Verify(m => m.Map<UserResponseDto>(It.IsAny<UserEntity>()), Times.Never);
        }
    }
}
