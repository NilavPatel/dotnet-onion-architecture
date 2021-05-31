using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.Interfaces.Services;
using MyApp.Application.Models.Requests;
using MyApp.Application.Models.Responses;

namespace MyApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserRes>> CreateUser(CreateUserReq user)
        {
            var result = await _userService.CreateUser(user);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetAllUsersRes>> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<GetUserByIdRes>> GetUserById(Guid id)
        {
            var result = await _userService.GetUserById(id);
            return Ok(result);
        }
    }
}
