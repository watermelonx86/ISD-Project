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
        [HttpPost("add-customer")]
        public Task<IActionResult> AddCustomer(CustomerDto request)
        {
            return _userService.AddCustomer(request);
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

        [HttpGet("get-customer-pending-approval")]
        public Task<IActionResult> GetCustomerPendingApproval()
        {
            return _userService.GetCustomerPendingApproval();
        }

        [HttpGet("get-customer-approved")]
        public Task<IActionResult> GetCustomerApproved()
        {
            return _userService.GetCustomerApproved();
        }

        [HttpGet("get-customer-rejected")]
        public Task<IActionResult> GetCustomerRejected()
        {
            return _userService.GetCustomerRejected();
        }

        [HttpDelete("delete-customer/{id}")]
        public Task<IActionResult> DeleteCustomer(int id)
        {
            return _userService.DeleteCustomer(id);
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

        [HttpGet("get-customer/{id}")]
        public Task<IActionResult> GetCustomer(int id)
        {
            return _userService.GetCustomer(id);
        }
        [HttpGet("get-health-info-customer/{id}")]
        public Task<IActionResult> GetHealthInformationOfCustomer(int id)
        {
            return _userService.GetHealthInformationOfCustomer(id);
        }
    }
}
