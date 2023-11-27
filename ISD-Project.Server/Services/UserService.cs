using ISD_Project.Server.DataAccess;
using ISD_Project.Server.Models;
using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISD_Project.Server.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> GetCustomer()
        {
            try
            {
                var listCustomer = await _dbContext.Customers.ToListAsync();
                var listCustomerDto = new List<UserDto>();
                foreach (var customer in listCustomer)
                {
                    var customerDto = new UserDto
                    {
                        Name = customer.Name,
                        Gender = customer.Gender,
                        Address = customer.Address,
                        Email = customer.Email,
                        IdentityDocumentId = customer.IdentityDocumentId,
                        PhoneNumber = customer.PhoneNumber,
                        
                    };
                    listCustomerDto.Add(customerDto);
                }
                return new OkObjectResult(listCustomerDto);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> GetUser()
        {
            try
            { 
                var listUser = await _dbContext.Users.ToListAsync();
                var listUserDto = new List<UserDto>();
                foreach(var user in listUser)
                {
                    var userDto = new UserDto
                    {
                        Name = user.Name,
                        Gender = user.Gender,
                        Address = user.Address,
                        Email = user.Email,
                        IdentityDocumentId = user.IdentityDocumentId,
                        PhoneNumber = user.PhoneNumber
                    };
                    listUserDto.Add(userDto);
                }
                return new OkObjectResult(listUserDto);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        public async Task<IActionResult> AddCustomer(UserDto request)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (request is null)
                    {
                        return new BadRequestObjectResult("Request is null");
                    }
                    if (await _dbContext.Users.AnyAsync(u => u.Email == request.Email))
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
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new OkObjectResult("Customer successfully created");

                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return new StatusCodeResult(500); // Internal Server Error

                }
            }
        }

        public async Task<IActionResult> AddCustomerCareDept(UserDto request)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    if (request is null)
                    {
                        return new BadRequestObjectResult("Request is null");
                    }
                    if (await _dbContext.Users.AnyAsync(u => u.Email == request.Email))
                    {
                        return new BadRequestObjectResult("Email already exists");
                    }
                    var customer = new CustomerCareDepartment
                    {
                        Name = request.Name,
                        Gender = request.Gender,
                        Address = request.Address,
                        Email = request.Email,
                        IdentityDocumentId = request.IdentityDocumentId,
                        PhoneNumber = request.PhoneNumber
                    };
                    _dbContext.CustomerCareDepartments.Add(customer);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new OkObjectResult("Customer Care Department successfully created");

                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return new StatusCodeResult(500); // Internal Server Error

                }
            }
        }

    }
}
