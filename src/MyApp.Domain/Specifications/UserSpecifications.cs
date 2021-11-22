using MyApp.Domain.Entities;

namespace MyApp.Domain.Specifications
{
    public static class UserSpecifications
    {
        public static BaseSpecification<User> UserByEmailAndPasswordSpec(string emailId, string password)
        {
            return new BaseSpecification<User>(x => x.EmailId == emailId && x.Password == password);
        }
    }
}
