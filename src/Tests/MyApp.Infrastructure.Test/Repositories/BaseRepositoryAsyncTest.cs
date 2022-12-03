using Xunit;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Repositories;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;

namespace MyApp.Infrastructure.Test.Repositories
{
    public class BaseRepositoryAsyncTest
    {
        private MyAppDbContext _myAppDbContext;
        private UnitOfWork _unitOfWork;

        public BaseRepositoryAsyncTest()
        {
            var options = new DbContextOptionsBuilder<MyAppDbContext>().UseInMemoryDatabase(databaseName: "MyAppDb").Options;
            _myAppDbContext = new MyAppDbContext(options);
            _unitOfWork = new UnitOfWork(_myAppDbContext);
        }

        [Fact]
        public async void AddAsync_WithValidData_SuccessfullyInsertData()
        {
            //Given
            var user = new User
            {
                FirstName = "Nilav",
                LastName = "Patel",
                EmailId = "nilavpatel1992@gmail.com",
                Password = "Test123",
                Status = UserStatus.Active,
                CreatedBy = Guid.NewGuid(),
                CreatedOn = DateTimeOffset.UtcNow
            };

            //When
            var result = await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            //Then
            Assert.Equal(result, _myAppDbContext.Users.Find(result.Id));
        }
    }
}