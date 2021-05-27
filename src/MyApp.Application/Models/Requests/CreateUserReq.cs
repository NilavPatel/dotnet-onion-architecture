using System;

namespace MyApp.Application.Models.Requests
{
    public class CreateUserReq
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}
