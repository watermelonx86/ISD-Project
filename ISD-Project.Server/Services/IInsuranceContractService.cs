using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server;

public interface IInsuranceContractService
{
    Task<IActionResult> AddInsuranceContractAsync(InsuranceContractDto insuranceContract);
    Task<IActionResult> DeleteInsuranceContractAsync(int id);
    Task<IActionResult> GetInsuranceContractAsync(int id);
    Task<IActionResult> GetInsuranceContractsAsync();
    Task<IActionResult> UpdateInsuranceContractAsync(InsuranceContract insuranceContract);
}
