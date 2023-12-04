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
        public ValidationService(ApplicationDbContext dbContext, IUserAccountService userAccount)
        {
            _dbContext = dbContext;
            _userAccount = userAccount;
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
                userAccount.IsActivated = (int) request.AccountStatus;
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
            try
            {
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == request.CustomerId);
                if (customer is null)
                {
                    return new NotFoundObjectResult("Customer not found");
                }
                customer.IsApproved = (int) request.ProfileStatus;
                _dbContext.Update(customer);
                await _dbContext.SaveChangesAsync();
                //TODO: Send email to customer
                //Create new account for customer after approval
                if(request.ProfileStatus == ProfileStatus.Approved)
                {
                    var userAccount = await _dbContext.UserAccounts.FirstOrDefaultAsync(u => u.Email == customer.Email);
                    if(userAccount is null)
                    {
                        var userRegisterRequest = new UserRegisterRequest
                        {
                            Email = customer.Email,
                            Password = "Demo123",
                            ConfirmPassword = "Demo123"
                        };
                        await _userAccount.Register(userRegisterRequest);
                        //Update user id in user account table
                        userAccount = await _dbContext.UserAccounts.FirstOrDefaultAsync(u => u.Email == customer.Email);
                        if(userAccount is not null)
                        {
                            userAccount.UserId = customer.Id;
                            _dbContext.Update(userAccount);
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                }
                return new OkObjectResult($"Customer information updated successfully: {request.ProfileStatus}");
            }
            catch(Exception) {
                return new StatusCodeResult(500);
            }
        }
    }
}
