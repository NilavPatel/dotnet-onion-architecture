using MyApp.Application.Models.Requests;
using MyApp.Application.Models.Responses;
using MyApp.Domain.Specifications;
using MyApp.Domain.Entities;
using MyApp.Domain.Enums;
using MyApp.Domain.Exceptions;
using MyApp.Application.Models.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Application.Core.Repositories;
using MyApp.Application.Core.Services;

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
            var user = await _unitOfWork.Repository<User>().AddAsync(new User
            {
                FirstName = req.FirstName,
                LastName = req.LastName,
                EmailId = req.EmailId,
                Password = req.Password,
                Status = req.Status
            });
            await _unitOfWork.SaveChangesAsync();
            return new CreateUserRes() { Data = new UserDTO(user) };
        }

        public async Task<ValidateUserRes> ValidateUser(ValidateUserReq req)
        {
            var validateUserSpec = UserSpecifications.GetUserByEmailAndPasswordSpec(req.EmailId, req.Password);
            var user = await _unitOfWork.Repository<User>().FirstOrDefaultAsync(validateUserSpec);
            if (user == null)
            {
                throw new UserNotFoundException();
            }
            if (user.Status != UserStatus.Active)
            {
                throw new UserIsNotActiveException();
            }
            return new ValidateUserRes()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public async Task<GetAllActiveUsersRes> GetAllActiveUsers()
        {
            var activeUsersSpec = UserSpecifications.GetAllActiveUsersSpec();
            var users = await _unitOfWork.Repository<User>().ListAsync(activeUsersSpec);

            return new GetAllActiveUsersRes()
            {
                Data = users.Select(x => new UserDTO(x)).ToList()
            };
        }
    }
}