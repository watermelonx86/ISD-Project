using ISD_Project.Server.DataAccess;
using ISD_Project.Server.Models;
using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISD_Project.Server.Services
{
    public class ValidationService : IValidationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IUserAccountService _userAccount;
        private readonly IEmailService _emailService;
        public ValidationService(ApplicationDbContext dbContext, IUserAccountService userAccount, IEmailService emailService)
        {
            _dbContext = dbContext;
            _userAccount = userAccount;
            _emailService = emailService;
        }

        public async Task<IActionResult> ValidateUserAccount(UserAccountValidateRequest request)
        {
            try
            {
                var userAccount = await _dbContext.UserAccounts.FirstOrDefaultAsync(u => u.Id == request.UserAccountId);
                if (userAccount is null)
                {
                    return new NotFoundObjectResult("User account not found");
                }
                userAccount.IsActivated = (int)request.AccountStatus;
                _dbContext.Update(userAccount);
                await _dbContext.SaveChangesAsync();
                return new OkObjectResult($"User Account information updated successfully: {request.AccountStatus}");
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> ValidateCustomer(CustomerValidateRequest request)
        {
            if (request.ProfileStatus == ProfileStatus.Approved)
            {
                try
                {
                    var customer = await _dbContext.Customers
                         .Include(uc => uc.UserAccount)
                        .FirstOrDefaultAsync(c => c.Id == request.CustomerId);
                    if (customer is null)
                    {
                        return new NotFoundObjectResult("Customer not found");
                    }
                    customer.IsApproved = (int)request.ProfileStatus;
                    _dbContext.Update(customer);
                    await _dbContext.SaveChangesAsync();

                    //Create new account for customer after approval
                    if (customer.UserAccount is null)
                    {
                        await CreateAndAssignUserAccountForCustomer(customer);
                        //TODO: Edit email body message
                        await _emailService.SendEmail(customer.Email, "Account created", EmailMessageBody.ProfileApproved(customer.UserAccount.Email, "Demo123", $"https://localhost:5173/activate/{customer.UserAccount.Id}"));
                    }

                    var response = new { userAccountId = customer.UserAccount?.Id, message = $"Validate customer successfully {request.ProfileStatus}" };
                    return new OkObjectResult(response);
                }
                catch (Exception)
                {
                    return new StatusCodeResult(500);
                }
            }
            else
            {
                return new OkObjectResult($"Customer information updated successfully: {request.ProfileStatus}");

            }

        }

        public async Task CreateAndAssignUserAccountForCustomer(Customer customer)
        {
            var existingUserAccount = await _dbContext.UserAccounts.FirstOrDefaultAsync(u => u.Email == customer.Email);
            if (existingUserAccount is null)
            {
                var userRegisterRequest = new UserAccountRegisterRequest(customer.Email, "Demo123", "Demo123", RoleType.Customer);
                await _userAccount.Register(userRegisterRequest);

                customer.UserAccount = await _dbContext.UserAccounts.FirstOrDefaultAsync(u => u.Email == customer.Email);

                if (customer.UserAccount is not null)
                {
                    customer.UserAccountId = customer.UserAccount.Id;
                    _dbContext.Update(customer);
                    await _dbContext.SaveChangesAsync();
                }
            }
            else
            {
                customer.UserAccount = existingUserAccount;
                customer.UserAccountId = existingUserAccount.Id;
                _dbContext.Update(customer);
                await _dbContext.SaveChangesAsync();
            }

        }
    }
}
