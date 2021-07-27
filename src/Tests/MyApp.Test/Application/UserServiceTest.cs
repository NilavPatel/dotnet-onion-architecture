using Xunit;
using Microsoft.EntityFrameworkCore;
using MyApp.Application.Services;
using MyApp.Application.Models.Requests;
using MyApp.Application.Models.Responses;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Repositories;
using MyApp.Test.Infrastructure;

namespace MyApp.Test.Service
{
    public class UserServiceTest
    {
        private MyAppDbContext _myAppDbContext;
        private UnitOfWork _unitOfWork;
        private UserService _userService;

        public UserServiceTest()
        {
            var options = new DbContextOptionsBuilder<MyAppDbContext>().UseInMemoryDatabase(databaseName: "MyAppDb").Options;
            _myAppDbContext = new MyAppDbContext(options);
            _unitOfWork = new UnitOfWork(_myAppDbContext);
            _userService = new UserService(_unitOfWork, new FakeLoggerService());
        }

        [Fact]
        public async void CreateUser_WithValidData_SuccessfullyCreateUser()
        {
            //Given
            var req = new CreateUserReq
            {
                FirstName = "Nilav",
                LastName = "Patel",
                EmailId = "nilavpatel1992@gmail.com",
                Password = "Test123"
            };

            //When
            var result = await _userService.CreateUser(req);

            //Then
            Assert.NotNull(result);
            Assert.Equal(typeof(CreateUserRes), result.GetType());
            Assert.NotNull(result.Data);
        }
    }
}