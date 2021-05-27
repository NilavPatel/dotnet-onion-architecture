using System.Threading.Tasks;
using MyApp.Application.Interfaces.Repositories;
using MyApp.Application.Interfaces.Services;
using MyApp.Domain.Models;

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

        public async Task<User> CreateUser(User user)
        {
            return await _userRepository.AddAsync(user);
        }
    }
}