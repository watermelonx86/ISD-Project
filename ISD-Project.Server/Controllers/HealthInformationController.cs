using ISD_Project.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthInformationController : ControllerBase
    {
         private readonly IHealthInformationService _healthInfor;
        public HealthInformationController(IHealthInformationService healthInformationService)
        {
                this._healthInfor = healthInformationService;
        }
         [HttpGet("CurrentMedications")]
         public ActionResult<List<string>> GetCurrentMedications()
         {
            return Ok(_healthInfor.GetCurrentMedications().Value);
         }

        [HttpGet("MedicalHistory")]
        public ActionResult<List<string>> GetMedicalHistory()
        {
            return Ok(_healthInfor.GetMedicalHistory().Value);
        }

        [HttpGet("VaccinationHistory")]
        public ActionResult<List<string>> GetVaccinationHistory()
        {
            return Ok(_healthInfor.GetVaccinationHistory().Value);
        }

        [HttpGet("LifestyleHabits")]
        public ActionResult<List<string>> LifestyleHabits()
        {
            return Ok(_healthInfor.LifestyleHabits().Value);
        }
    }
}
