using Xunit;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyApp.Domain.Specifications;
using MyApp.Domain.Models;

namespace MyApp.Test.Domain
{
    public class UserSpecificationsTest
    {
        [Fact]
        public void ValidateUser_WithValidData_ReturnValidData()
        {
            //Given
            List<User> users = new List<User>{
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
            
            //When
            var spec = UserSpecifications.ValidateUser("nilavpatel1992@gmail.com", "Test123");
            var result = users.AsQueryable().Where(spec.Criteria).Count();

            //Then
            Assert.Equal(1, result);
        }
    }
}