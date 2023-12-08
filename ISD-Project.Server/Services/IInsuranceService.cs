using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services
{
    public interface IInsuranceService
    {
        Task<IActionResult> GetInsurance();

    }
}
