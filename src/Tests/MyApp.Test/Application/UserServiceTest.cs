using Xunit;
using Microsoft.EntityFrameworkCore;
using MyApp.Application.Services;
using MyApp.Application.Models.Requests;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Repositories;
using MyApp.Test.Infrastructure;

namespace MyApp.Test.Service
{
    public class UserServiceTest
    {
        private MyAppDbContext myAppDbContext;
        private UnitOfWork unitOfWork;
        private UserService userService;
        public UserServiceTest()
        {
            var options = new DbContextOptionsBuilder<MyAppDbContext>().UseInMemoryDatabase(databaseName: "MyAppDb").Options;
            myAppDbContext = new MyAppDbContext(options);
            unitOfWork = new UnitOfWork(myAppDbContext);
            userService = new UserService(unitOfWork, new FakeLoggerService());
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
            var result = await userService.CreateUser(req);

            //Then
            Assert.NotNull(result);
        }
    }
}