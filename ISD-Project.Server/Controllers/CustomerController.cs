using ISD_Project.Server.Models.DTOs;
using ISD_Project.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;

        }

        [HttpPost("add-customer")]
        public Task<IActionResult> AddCustomer(CustomerRegisterRequest request)
        {
            return _customerService.AddCustomerAsync(request);
        }

        [HttpDelete("delete-customer/{id}")]
        public Task<IActionResult> DeleteCustomer(int id)
        {
            return _customerService.DeleteCustomerForceAsync(id);
        }
        [HttpGet("get-customer")]
        public Task<IActionResult> GetCustomer()
        {
            return _customerService.GetCustomerAsync();
        }

        [HttpGet("get-customer/{id}")]
        public Task<IActionResult> GetCustomer(int id)
        {
            return _customerService.GetCustomerAsync(id);
        }

        [HttpGet("get-health-info-customer/{id}")]
        public Task<IActionResult> GetHealthInformationOfCustomer(int id)
        {
            return _customerService.GetHealthInformationOfCustomerAsync(id);
        }

        [HttpGet("get-customer-pending-approval")]
        public Task<IActionResult> GetCustomerPendingApproval()
        {
            return _customerService.GetCustomerPendingApprovalAsync();
        }

        [HttpGet("get-customer-approved")]
        public Task<IActionResult> GetCustomerApproved()
        {
            return _customerService.GetCustomerApprovedAsync();
        }

        [HttpGet("get-customer-rejected")]
        public Task<IActionResult> GetCustomerRejected()
        {
            return _customerService.GetCustomerRejectedAsync();
        }
    }
}
