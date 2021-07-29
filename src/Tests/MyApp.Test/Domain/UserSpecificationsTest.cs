using Xunit;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Specifications;
using MyApp.Domain.Models;
using MyApp.Infrastructure.Repositories;

namespace MyApp.Test.Domain
{
    public class UserSpecificationsTest
    {
        private List<User> _users;
        public UserSpecificationsTest()
        {
            _users = new List<User>{
                new User
                {
                    FirstName = "Nilav",
                    LastName = "Patel",
                    EmailId = "nilavpatel1992@gmail.com",
                    Password = "Test123",
                    IsActive = true
                },
                new User
                {
                    FirstName = "Nilav1",
                    LastName = "Patel",
                    EmailId = "nilav1patel1992@gmail.com",
                    Password = "Test1234",
                    IsActive = false
                },
                new User
                {
                    FirstName = "Nilav2",
                    LastName = "Patel",
                    EmailId = "nilav2patel1992@gmail.com",
                    Password = "Test1235",
                    IsActive = false
                }
            };
        }

        [Fact]
        public void ValidateUser_WithValidData_ReturnValidData()
        {
            //Given
            var spec = UserSpecifications.ValidateUser("nilavpatel1992@gmail.com", "Test123");

            //When
            var count = SpecificationEvaluator<User>.GetQuery(_users.AsQueryable(), spec).Count();

            //Then
            Assert.Equal(1, count);
        }
    }
}