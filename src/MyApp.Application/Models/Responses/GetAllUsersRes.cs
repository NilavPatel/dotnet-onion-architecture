using System.Collections.Generic;
using MyApp.Domain.Models;

namespace MyApp.Application.Models.Responses
{
    public class GetAllUsers
    {
        public IEnumerable<User> Data { get; set; }
    }
}
