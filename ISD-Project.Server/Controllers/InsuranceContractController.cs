using ISD_Project.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceContractController : ControllerBase
    {
        private readonly IInsuranceContractService _insuranceContractService;
        public InsuranceContractController(IInsuranceContractService insuranceContractService)
        {
            _insuranceContractService = insuranceContractService;
        }

        [HttpGet("get-insurance-contract")]
        public Task<IActionResult> GetAllInsuranceContract()
        {
            return _insuranceContractService.GetInsuranceContractsAsync();
        }

        [HttpPost("add-insurance-contract")]
        public Task<IActionResult> AddInsuranceContract(InsuranceContractDto insuranceContractDto)
        {
            return _insuranceContractService.AddInsuranceContractAsync(insuranceContractDto);
        }
    }
}
