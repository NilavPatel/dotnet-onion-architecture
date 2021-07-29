using System;
using Xunit;
using Moq;
using System.Threading.Tasks;
using MyApp.Application.Services;
using MyApp.Application.Models.Requests;
using MyApp.Application.Models.Responses;
using MyApp.Application.Interfaces.Repositories;
using MyApp.Application.Interfaces.Services;
using MyApp.Domain.Models;

namespace MyApp.Test.Service
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
                IsActive = false
            });
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            //When
            var result = await _userService.CreateUser(new CreateUserReq
            {
                FirstName = "Nilav",
                LastName = "Patel",
                EmailId = "nilavpatel1992@gmail.com",
                Password = "Test123",
            });

            //Then
            Assert.NotNull(result);
            Assert.NotNull(result.Data);
            Assert.Equal(id, result.Data.Id);
        }
    }
}