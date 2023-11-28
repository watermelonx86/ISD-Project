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

        [HttpPost("add_financialdept")]
        public Task<IActionResult> FinancialDeptRegister(FinancialDto request)
        {
            return _userService.FinancialDeptAdd(request);
        }

        [HttpPost("add_validationdept")]
        public Task<IActionResult> ValdationDeptRegister(ValidationDto request)
        {
            return _userService.ValidationDeptAdd(request);
        }

        [HttpGet("get_user")]
        public Task<IActionResult> GetUser()
        {
            return _userService.GetUser();
        }

        [HttpGet("get-customer")]
        public Task<IActionResult> GetCustomer()
        {
            return _userService.GetCustomer();
        }

        [HttpDelete("delete_customer/{id}")]
        public Task<IActionResult> DeleteCustomer(int id)
        {
            return _userService.DeleteCustomer(id);
        }

        [HttpDelete("delete_customercare/{id}")]
        public Task<IActionResult> DeleteCustomerCare(int id)
        {
            return _userService.DeleteCustomerCare(id);
        }

        [HttpDelete("delete_financialdept/{id}")]
        public Task<IActionResult> DeleteFinancialDept(int id)
        {
            return _userService.DeleteFinancialDept(id);
        }

        [HttpDelete("delete_validationdept/{id}")]
        public Task<IActionResult> DeleteValdationDept(int id)
        {
            return _userService.DeleteValdationDept(id);
        }

        [HttpGet("get-customer/{id}")]
        public Task<IActionResult> GetCustomer(int id)
        {
            return _userService.GetCustomer(id);
        }
    }
}
