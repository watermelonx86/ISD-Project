using ISD_Project.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthInformationController : ControllerBase
    {
        private readonly IHealthInformationService _healthInformation;
        public HealthInformationController(IHealthInformationService healthInformationService)
        {
            this._healthInformation = healthInformationService;
        }

    }
}
