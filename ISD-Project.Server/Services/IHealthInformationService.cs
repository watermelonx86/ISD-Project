using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services
{
    public interface IHealthInformationService
    {
       public Task<IActionResult> AddHealthInformation(HealthInformationDto request);
    }
}
