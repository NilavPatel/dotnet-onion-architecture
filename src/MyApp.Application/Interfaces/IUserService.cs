using MyApp.Application.Models.Requests;
using MyApp.Application.Models.Responses;

namespace MyApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<CreateUserRes> CreateUser(CreateUserReq req);

        Task<ValidateUserRes> ValidateUser(ValidateUserReq req);

        Task<GetAllActiveUsersRes> GetAllActiveUsers();
    }
}