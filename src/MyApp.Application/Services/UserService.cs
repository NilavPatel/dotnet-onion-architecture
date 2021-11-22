using System.Threading.Tasks;
using MyApp.Application.Interfaces.Repositories;
using MyApp.Application.Interfaces.Services;
using MyApp.Application.Models.Requests;
using MyApp.Application.Models.Responses;
using MyApp.Domain.Specifications;
using MyApp.Domain.Entities;

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

        public async Task<CreateUserRes> CreateUser(CreateUserReq req)
        {
            await using (_unitOfWork)
            {
                var user = await _unitOfWork.Repository<User>().AddAsync(new User
                {
                    FirstName = req.FirstName,
                    LastName = req.LastName,
                    EmailId = req.EmailId,
                    Password = req.Password,
                    IsActive = false
                });
                await _unitOfWork.SaveChangesAsync();
                return new CreateUserRes() { Data = user };
            }
        }

        public async Task<ValidateUserRes> ValidateUser(ValidateUserReq req)
        {
            await using (_unitOfWork)
            {
                var validateUserSpec = UserSpecifications.UserByEmailAndPasswordSpec(req.EmailId, req.Password);
                var user = await _unitOfWork.Repository<User>().FirstOrDefaultAsync(validateUserSpec);
                if (user == null)
                {
                    return null;
                }
                return new ValidateUserRes()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
            }
        }
    }
}