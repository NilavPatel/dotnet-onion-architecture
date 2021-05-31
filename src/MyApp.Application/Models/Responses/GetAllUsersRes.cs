using System.Collections.Generic;
using MyApp.Domain.Models;

namespace MyApp.Application.Models.Responses
{
    public class GetAllUsersRes
    {
        public IEnumerable<User> Data { get; set; }
    }
}
