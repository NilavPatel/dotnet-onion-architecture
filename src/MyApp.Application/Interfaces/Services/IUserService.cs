using System.Threading.Tasks;
using MyApp.Domain.Models;

namespace MyApp.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> CreateUser(User user);
    }
}