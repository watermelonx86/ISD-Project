using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services
{
    public interface ICustomerService
    {
        Task<IActionResult> GetCustomer();
        Task<IActionResult> GetCustomerPendingApproval();
        Task<IActionResult> GetCustomerApproved();
        Task<IActionResult> GetCustomerRejected();
        Task<IActionResult> GetCustomer(int id);
        Task<IActionResult> GetHealthInformationOfCustomer(int id);

        Task<IActionResult> AddCustomer(CustomerRegisterRequest request);
        Task<IActionResult> DeleteCustomer(int id);
    }
}
