using ISD_Project.Server.DataAccess;
using ISD_Project.Server.Models;
using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services
{
    public class UserService : IUserService
    {
        //Your code here
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult GetCustomer()
        {
            try
            {
                var listCustomer = _dbContext.Customers.ToList();
                return new OkObjectResult(listCustomer);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        public IActionResult GetUser()
        {
            try
            {
                var listUser = _dbContext.Users.ToList();
                return new OkObjectResult(listUser);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        public IActionResult CustomerRegister(CustomerRegisterRequest request)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    if(request is null)
                    {
                        return new BadRequestObjectResult("Request is null");
                    }
                    if(_dbContext.Users.Any(u => u.Email ==  request.Email))
                    {
                           return new BadRequestObjectResult("Email already exists");
                    }
                    var customer = new Customer
                    {
                        Name = request.Name,
                        Gender = request.Gender,
                        Address = request.Address,
                        Email = request.Email,
                        IdentityDocumentId = request.IdentityDocumentId,
                        PhoneNumber = request.PhoneNumber
                    };
                    _dbContext.Customers.Add(customer);
                    _dbContext.SaveChanges();
                    transaction.Commit();
                    return new OkObjectResult("Customer successfully created");

                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return new StatusCodeResult(500); // Internal Server Error

                }
            }
        }
    }
}
