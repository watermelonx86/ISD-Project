using ISD_Project.Server.Models;
using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services
{
    public interface IValidationService
    {
        Task<IActionResult> ValidateCustomerAsync(CustomerValidateRequest request);
        Task CreateAndAssignUserAccountForCustomerAsync(Customer customer);
        Task<IActionResult> ValidateUserAccountAsync(UserAccountValidateRequest request);
    }
}
