using ISD_Project.Server.Models.DTOs;
using ISD_Project.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        //TODO Your code here
        [HttpPost("add-customer")]
        public Task<IActionResult> AddCustomer(UserDto request)
        {
            return  _userService.AddCustomer(request);
        }

        [HttpPost("add-customercaredept")]
        public Task<IActionResult> AddCustomerCareDept(UserDto request)
        {
            return _userService.AddCustomerCareDept(request);
        }

        [HttpGet("get-user")]
        public Task<IActionResult> GetUser()
        {
            return _userService.GetUser();
        }

        [HttpGet("get-customer")]
        public Task<IActionResult> GetCustomer()
        {
            return _userService.GetCustomer();
        }
    }
}
