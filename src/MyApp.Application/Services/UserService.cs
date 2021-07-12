using System;
using System.Threading.Tasks;
using MyApp.Application.Interfaces.Repositories;
using MyApp.Application.Interfaces.Services;
using MyApp.Application.Models.Requests;
using MyApp.Application.Models.Responses;
using MyApp.Domain.Specifications;
using MyApp.Domain.Models;

namespace MyApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerService _loggerService;

        public UserService(IUnitOfWork unitOfWork, ILoggerService loggerService)
        {
            _unitOfWork = unitOfWork;
            _loggerService = loggerService;
        }

        public async Task<CreateUserRes> CreateUser(CreateUserReq userReq)
        {
            await using (_unitOfWork)
            {
                var user = await _unitOfWork.Repository<User>().AddAsync(new User
                {
                    FirstName = userReq.FirstName,
                    LastName = userReq.LastName,
                    EmailId = userReq.EmailId,
                    Password = userReq.Password
                });
                await _unitOfWork.SaveChangesAsync();
                return new CreateUserRes() { Id = user.Id };
            }
        }

        public async Task<GetAllUsersRes> GetAllUsers()
        {
            await using (_unitOfWork)
            {
                var data = await _unitOfWork.Repository<User>().ListAllAsync();
                return new GetAllUsersRes() { Data = data };
            }
        }

        public async Task<GetUserByIdRes> GetUserById(Guid id)
        {
            await using (_unitOfWork)
            {
                var getByIdSpec = UserSpecifications.GetById(id);
                var user = await _unitOfWork.Repository<User>().FirstOrDefaultAsync(getByIdSpec);
                if (user != null)
                {
                    return new GetUserByIdRes() { Data = user };
                }
                return new GetUserByIdRes();
            }
        }
    }
}