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
        public Task<IActionResult> AddCustomer(CustomerDto request)
        {
            return _customerService.AddCustomer(request);
        }

        [HttpDelete("delete-customer/{id}")]
        public Task<IActionResult> DeleteCustomer(int id)
        {
            return _customerService.DeleteCustomer(id);
        }
        [HttpGet("get-customer")]
        public Task<IActionResult> GetCustomer()
        {
            return _customerService.GetCustomer();
        }

        [HttpGet("get-customer-pending-approval")]
        public Task<IActionResult> GetCustomerPendingApproval()
        {
            return _customerService.GetCustomerPendingApproval();
        }

        [HttpGet("get-customer-approved")]
        public Task<IActionResult> GetCustomerApproved()
        {
            return _customerService.GetCustomerApproved();
        }

        [HttpGet("get-customer-rejected")]
        public Task<IActionResult> GetCustomerRejected()
        {
            return _customerService.GetCustomerRejected();
        }
    }
}
