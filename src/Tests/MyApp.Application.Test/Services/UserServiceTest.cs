using Xunit;
using MyApp.Application.Services;
using MyApp.Application.Models.Requests;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using MyApp.Domain.Exceptions;
using MyApp.Domain.Core.Specifications;
using MyApp.Application.Core.Services;
using MyApp.Application.Core.Repositories;
using Moq;

namespace MyApp.Application.Test.Services
{
    public class UserServiceTest
    {
        private Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();
        private Mock<ILoggerService> _loggerMock = new Mock<ILoggerService>();
        private UserService _userService;

        public UserServiceTest()
        {
            _userService = new UserService(_unitOfWorkMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async void CreateUser_WithValidData_SuccessfullyCreateUser()
        {
            //Given
            var id = Guid.NewGuid();
            _unitOfWorkMock.Setup(x => x.Repository<User>().AddAsync(It.IsAny<User>()))
                .ReturnsAsync(new User
                {
                    Id = id,
                    FirstName = "Nilav",
                    LastName = "Patel",
                    EmailId = "nilavpatel1992@gmail.com",
                    Password = "Test123",
                    Status = UserStatus.Active
                });
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            //When
            var result = await _userService.CreateUser(new CreateUserReq
            {
                FirstName = "Nilav",
                LastName = "Patel",
                EmailId = "nilavpatel1992@gmail.com",
                Password = "Test123",
                Status = UserStatus.Active
            });

            //Then
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal(id, result.Data.Id);
        }

        [Fact]
        public async void ValidateUser_UserNotExist_ThrowException()
        {
            // Given
            var id = Guid.NewGuid();
            User user = null;
            _unitOfWorkMock.Setup(x => x.Repository<User>().FirstOrDefaultAsync(It.IsAny<ISpecification<User>>()))
                .ReturnsAsync(user);

            // when
            await Assert.ThrowsAsync<UserNotFoundException>(async () => await _userService.ValidateUser(new ValidateUserReq
            {
                EmailId = "nilavpatel1992@gmail.com",
                Password = "Test123"
            }));
        }

        [Fact]
        public async void ValidateUser_UserIsNotActive_ThrowException()
        {
            // Given
            var id = Guid.NewGuid();
            User user = new User
            {
                Id = id,
                FirstName = "Nilav",
                LastName = "Patel",
                EmailId = "nilavpatel1992@gmail.com",
                Password = "Test123",
                Status = UserStatus.InActive
            };
            _unitOfWorkMock.Setup(x => x.Repository<User>().FirstOrDefaultAsync(It.IsAny<ISpecification<User>>()))
                .ReturnsAsync(user);

            // when
            await Assert.ThrowsAsync<UserIsNotActiveException>(async () => await _userService.ValidateUser(new ValidateUserReq
            {
                EmailId = "nilavpatel1992@gmail.com",
                Password = "Test123"
            }));
        }

        [Fact]
        public async void ValidateUser_ValidData_ReturnsResult()
        {
            // Given
            var id = Guid.NewGuid();
            User user = new User
            {
                Id = id,
                FirstName = "Nilav",
                LastName = "Patel",
                EmailId = "nilavpatel1992@gmail.com",
                Password = "Test123",
                Status = UserStatus.Active
            };
            _unitOfWorkMock.Setup(x => x.Repository<User>().FirstOrDefaultAsync(It.IsAny<ISpecification<User>>()))
                .ReturnsAsync(user);

            // when
            var result = await _userService.ValidateUser(new ValidateUserReq
            {
                EmailId = "nilavpatel1992@gmail.com",
                Password = "Test123"
            });

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }
    }
}