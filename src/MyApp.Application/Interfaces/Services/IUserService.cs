using System;
using System.Threading.Tasks;
using MyApp.Application.Models.Requests;
using MyApp.Application.Models.Responses;

namespace MyApp.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<CreateUserRes> CreateUser(CreateUserReq user);

        Task<GetAllUsersRes> GetAllUsers();

        Task<GetUserByIdRes> GetUserById(Guid id);

    }
}