using ISD_Project.Server.Models.DTOs;
using ISD_Project.Server.Services;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("get-user")]
        public Task<IActionResult> GetUser()
        {
            return _userService.GetUser();
        }

        [HttpPost("add-customercaredept")]
        public Task<IActionResult> AddCustomerCareDept(UserDto request)
        {
            return _userService.AddCustomerCareDept(request);
        }

        [HttpPost("add-financialdept")]
        public Task<IActionResult> FinancialDeptRegister(UserDto request)
        {
            return _userService.FinancialDeptAdd(request);
        }

        [HttpPost("add-validationdept")]
        public Task<IActionResult> ValdationDeptRegister(UserDto request)
        {
            return _userService.ValidationDeptAdd(request);
        }

        [HttpDelete("delete-customercare/{id}")]
        public Task<IActionResult> DeleteCustomerCare(int id)
        {
            return _userService.DeleteCustomerCare(id);
        }

        [HttpDelete("delete-financialdept/{id}")]
        public Task<IActionResult> DeleteFinancialDept(int id)
        {
            return _userService.DeleteFinancialDept(id);
        }

        [HttpDelete("delete-validationdept/{id}")]
        public Task<IActionResult> DeleteValidationDept(int id)
        {
            return _userService.DeleteValidationDept(id);
        }

    }
}
