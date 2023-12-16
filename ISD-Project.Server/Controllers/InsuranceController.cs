using ISD_Project.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceService _insuranceService;
        public InsuranceController(IInsuranceService insuranceService)
        {
            this._insuranceService = insuranceService;
        }

        [HttpGet("get-insurance/{id}")]
        public Task<IActionResult> GetInsurances(int id)
        {
            return _insuranceService.GetInsuranceByTypesAsync(id);
        }

        [HttpGet("get-insurance-detail/{id}")]
        public Task<IActionResult> GetInsurancesDetail(int id)
        {
            return _insuranceService.GetInsuranceDetailAsync(id);
        }

        [HttpGet("get-insurance-type")]
        public Task<IActionResult> GetInsurancesType()
        {
            return _insuranceService.GetInsuranceTypesAsync();
        }
    }
}
