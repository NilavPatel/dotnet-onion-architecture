using MyApp.Application.Models.DTOs;

namespace MyApp.Application.Models.Responses
{
    public class GetAllActiveUsersRes
    {
        public IList<UserDTO> Data { get; set; }
    }
}
