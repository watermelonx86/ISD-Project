using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services
{
    public interface IInsuranceService
    {
        Task<IActionResult> GetInsuranceByTypes(int id);
        Task<IActionResult> GetInsuranceDetail(int id);
        Task<IActionResult> GetInsuranceTypes();
    }
}
