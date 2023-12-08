using ISD_Project.Server.Services;
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

        [HttpGet("get-insurance")]
        public Task<IActionResult> GetInsurances()
        {
            return _insuranceService.GetInsurance();
        }
    }
}
