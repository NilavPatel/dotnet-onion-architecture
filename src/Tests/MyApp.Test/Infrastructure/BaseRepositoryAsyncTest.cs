using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using MyApp.Infrastructure.Data;
using MyApp.Infrastructure.Repositories;
using MyApp.Domain.Models;

namespace MyApp.Test.Infrastructure
{
    public class BaseRepositoryAsyncTest
    {
        private MyAppDbContext myAppDbContext;
        private UnitOfWork unitOfWork;
        public BaseRepositoryAsyncTest()
        {
            var options = new DbContextOptionsBuilder<MyAppDbContext>().UseInMemoryDatabase(databaseName: "MyAppDb").Options;
            myAppDbContext = new MyAppDbContext(options);
            unitOfWork = new UnitOfWork(myAppDbContext);
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
                IsActive = false
            };

            //When
            var result = await unitOfWork.Repository<User>().AddAsync(user);
            await unitOfWork.SaveChangesAsync();

            //Then
            Assert.Equal(result, myAppDbContext.Users.Find(result.Id));
        }
    }
}