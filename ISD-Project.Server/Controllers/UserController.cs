using ISD_Project.Server.Models.DTOs;
using ISD_Project.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Net.Http;

namespace ISD_Project.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        //TODO Your code here
        [HttpPost("add-customer")]
        public Task<IActionResult> AddCustomer(CustomerDto request)
        {
            // SendEmailNotification("watermelonx86@gmail.com"); //Test email
            return _userService.AddCustomer(request);
        }

        [HttpPost("add-customercaredept")]
        public Task<IActionResult> AddCustomerCareDept(UserDto request)
        {
            return _userService.AddCustomerCareDept(request);
        }

        [HttpPost("add-financialdept")]
        public Task<IActionResult> FinancialDeptRegister(UserDto request)
        {
            return _userService.FinancialDeptAdd(request);
        }

        [HttpPost("add-validationdept")]
        public Task<IActionResult> ValdationDeptRegister(UserDto request)
        {
            return _userService.ValidationDeptAdd(request);
        }

        [HttpGet("get-user")]
        public Task<IActionResult> GetUser()
        {
            return _userService.GetUser();
        }

        [HttpGet("get-customer")]
        public Task<IActionResult> GetCustomer()
        {
            return _userService.GetCustomer();
        }

        [HttpGet("get-customer-pending-approval")]
        public Task<IActionResult> GetCustomerPendingApproval()
        {
            return _userService.GetCustomerPendingApproval();
        }

        [HttpGet("get-customer-approved")]
        public Task<IActionResult> GetCustomerApproved()
        {
            return _userService.GetCustomerApproved();
        }

        [HttpGet("get-customer-rejected")]
        public Task<IActionResult> GetCustomerRejected()
        {
            return _userService.GetCustomerRejected();
        }

        [HttpDelete("delete-customer/{id}")]
        public Task<IActionResult> DeleteCustomer(int id)
        {
            return _userService.DeleteCustomer(id);
        }

        [HttpDelete("delete-customercare/{id}")]
        public Task<IActionResult> DeleteCustomerCare(int id)
        {
            return _userService.DeleteCustomerCare(id);
        }

        [HttpDelete("delete-financialdept/{id}")]
        public Task<IActionResult> DeleteFinancialDept(int id)
        {
            return _userService.DeleteFinancialDept(id);
        }

        [HttpDelete("delete-validationdept/{id}")]
        public Task<IActionResult> DeleteValidationDept(int id)
        {
            return _userService.DeleteValidationDept(id);
        }

        [HttpGet("get-customer/{id}")]
        public Task<IActionResult> GetCustomer(int id)
        {
            return _userService.GetCustomer(id);
        }

        [HttpGet("get-health-info-customer/{id}")]
        public Task<IActionResult> GetHealthInformationOfCustomer(int id)
        {
            return _userService.GetHealthInformationOfCustomer(id);
        }
        private void SendEmailNotification(string toEmail)
        {
            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("mpfitus@gmail.com", "wanifirqpvshynqg");
                smtpClient.EnableSsl = true;
                smtpClient.Port = 587;

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("mpfitus@gmail.com");
                    mailMessage.To.Add(toEmail);
                    mailMessage.Subject = "Customer Form Was Accepted";
                    mailMessage.Body = @"
                    <p> <strong>Thank you</strong> for registering an account in our system :)
                    <br /><strong>Username:</strong> user@example.com<br /><strong>Password:</strong> Demo123 </p>
                    <p>Price for Insurance</p> 
                    <table border='1'>
                        <thead>
                            <tr>
                                <th>Tên bảo hiểm</th>
                                <th>Loại bảo hiểm</th>
                                <th>Thành tiền</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Row 1</td>
                                <td>Row 1</td>
                                <td>Row 1</td>
                            </tr>
                            <tr>
                                <td>Row 2</td>
                                <td>Row 2</td>
                                <td>Row 2</td>
                            </tr>
                        </tbody>
                    </table>";

                    mailMessage.IsBodyHtml = true;

                    smtpClient.Send(mailMessage);
                }
            }
        }

    }
}
