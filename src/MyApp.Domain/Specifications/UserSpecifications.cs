using MyApp.Domain.Models;

namespace MyApp.Domain.Specifications
{
    public static class UserSpecifications
    {
        public static BaseSpecification<User> ValidateUser(string emailId, string password)
        {
            return new BaseSpecification<User>(x => x.EmailId == emailId && x.Password == password);
        }
    }
}
