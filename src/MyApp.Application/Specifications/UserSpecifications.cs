using System;
using MyApp.Domain.Models;

namespace MyApp.Application.Specifications
{
    public static class UserSpecifications
    {
        public static BaseSpecification<User> GetById(Guid id)
        {
            return new BaseSpecification<User>(x => x.Id == id);
        }
    }
}
