using AutoMapper;
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
        private readonly IMapper _mapper;
        public UserService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetUser()
        {
            try
            { 
                var listUser = await _dbContext.Users.ToListAsync();
                var listUserDto = _mapper.Map<List<UserDto>>(listUser);
                return new OkObjectResult(listUserDto);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
        public async Task<IActionResult> GetCustomer()
        {
            try
            {
                var listCustomer = await _dbContext.Customers.ToListAsync();
                var listCustomerDto = _mapper.Map<List<UserDto>>(listCustomer);
                return new OkObjectResult(listCustomerDto);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
        public async Task<IActionResult> GetCustomer(int id)
        {
            try
            {
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
                if(customer is null)
                {
                    return new NotFoundObjectResult("Customer not found");
                } else
                {
                    var customerDto = _mapper.Map<UserDto>(customer);
                    return new OkObjectResult(customerDto);
                }
                
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
                    var customer = _mapper.Map<Customer>(request);
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
                    var customerCareDept = _mapper.Map<CustomerCareDepartment>(request);
                    _dbContext.CustomerCareDepartments.Add(customerCareDept);
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
