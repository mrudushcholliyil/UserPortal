using AutoMapper;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserPortal.Application.Common.Interfaces;
using UserPortal.Application.DTOs.Response;
using UserPortal.Application.User.Queries.GetUserById;
using UserPortal.Domain.Entities;

namespace UserPortal.Application.UnitTest.Queries
{

    [TestFixture]
    public class GetUserByIdQueryHandlerTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private GetUserByIdQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new GetUserByIdQueryHandler(_userRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public async Task Handle_UserExists_ReturnsUserResponseDto()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var userEntity = new UserEntity { Id = userId, FirstName = "mrudush", LastName = "Cholliyil", Email = "mrudushc@gmail.com", Phone = "1234567890" };
            var userResponseDto = new UserResponseDto (Id : userId, FirstName : "mrudush", LastName : "Cholliyil", Email : "mrudushc@gmail.com", Phone : "1234567890" );

            _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(userEntity);
            _mapperMock.Setup(m => m.Map<UserResponseDto>(userEntity)).Returns(userResponseDto);

            var query = new GetUserByIdQuery(userId);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(userResponseDto);
        }

        [Test]
        public async Task Handle_UserDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync((UserEntity)null);

            var query = new GetUserByIdQuery(userId);

            // Act
            Func<Task> act = async () => await _handler.Handle(query, CancellationToken.None);

            // Assert
            await act.Should().ThrowAsync<KeyNotFoundException>()
                .WithMessage($"User with ID {userId} was not found.");
        }
    }
}
