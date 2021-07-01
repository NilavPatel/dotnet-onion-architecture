using System.Threading.Tasks;
using MyApp.Application.Interfaces.Repositories;
using MyApp.Application.Interfaces.Services;
using MyApp.Domain.Models;
using MyApp.Application.Models.Requests;
using MyApp.Application.Models.Responses;
using System;
using MyApp.Domain.Specifications;

namespace MyApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepositoryAsync<User> _userRepository;
        private readonly ILoggerService _loggerService;

        public UserService(IBaseRepositoryAsync<User> userRepository, ILoggerService loggerService)
        {
            _userRepository = userRepository;
            _loggerService = loggerService;
        }

        public async Task<CreateUserRes> CreateUser(CreateUserReq userReq)
        {
            var user = await _userRepository.AddAsync(new User
            {
                FirstName = userReq.FirstName,
                LastName = userReq.LastName,
                EmailId = userReq.EmailId,
                Password = userReq.Password
            });
            return new CreateUserRes() { Id = user.Id };
        }

        public async Task<GetAllUsersRes> GetAllUsers()
        {
            var data = await _userRepository.ListAllAsync();
            return new GetAllUsersRes() { Data = data };
        }

        public async Task<GetUserByIdRes> GetUserById(Guid id)
        {
            var getByIdSpec = UserSpecifications.GetById(id);
            var user = await _userRepository.FirstOrDefaultAsync(getByIdSpec);
            if (user != null)
            {
                return new GetUserByIdRes() { Data = user };
            }
            return new GetUserByIdRes();
        }
    }
}