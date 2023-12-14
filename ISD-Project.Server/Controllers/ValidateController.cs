using ISD_Project.Server.Models.DTOs;
using ISD_Project.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValidateController : ControllerBase
    {
        private readonly IValidationService _validationService;
        public ValidateController(IValidationService validationService)
        {
            this._validationService = validationService;
        }
        // [HttpPost("validate-customer")]
        // public Task<IActionResult> ValidateCustomer(CustomerValidateRequest request)
        // {
        //     return _validationService.ValidateCustomerAsync(request);
        // }
        [HttpPost("validate-user-account")]
        public Task<IActionResult> ValidateUserAccount(UserAccountValidateRequest request)
        {
            return _validationService.ValidateUserAccountAsync(request);
        }
    }
}
