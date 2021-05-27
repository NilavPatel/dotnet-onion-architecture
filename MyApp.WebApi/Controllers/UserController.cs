using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Interfaces.Services;
using MyApp.Domain.Models;

namespace MyApp.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            var result = await _userService.CreateUser(user);
            return Ok(result);
        }
    }
}
