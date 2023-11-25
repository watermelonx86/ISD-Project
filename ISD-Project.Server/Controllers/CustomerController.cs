using ISD_Project.Server.Models.DTOs;
using ISD_Project.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUserService _userService;
        public CustomerController(IUserService userService)
        {
            this._userService = userService;
        }
        //TODO Your code here
        [HttpPost("customer_register")]
        public Task<IActionResult> CustomerRegister(CustomerRegisterRequest request)
        {
            return  _userService.CustomerRegister(request);
        }

        [HttpGet("get_user")]
        public Task<IActionResult> GetUser()
        {
            return _userService.GetUser();
        }

        [HttpGet("get_customer")]
        public Task<IActionResult> GetCustomer()
        {
            return _userService.GetCustomer();
        }
    }
}
