using AutoMapper;
using ISD_Project.Server.DataAccess;
using ISD_Project.Server.Models.DTOs;
using ISD_Project.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISD_Project.Server.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IActionResult> AddCustomer(CustomerDto request)
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

                    customer.HealthInformation = new HealthInformation(); // After created customer, create health information for customer
                    customer.HealthInformation.CustomerId = customer.Id;
                    customer.HealthInformation.LastUpdate = DateTime.UtcNow;
                    _dbContext.Customers.Update(customer);
                    await _dbContext.SaveChangesAsync();

                    await transaction.CommitAsync();

                    var response = new { userId = customer.Id, message = "Customer successfully created" };
                    return new OkObjectResult(response);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new ObjectResult(ex.Message)
                    {
                        StatusCode = 500 // Internal Server Error
                    };
                }
            }
        }

        //BUG
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var customer = await _dbContext.Customers.FindAsync(id);

                    if (customer is null)
                    {
                        return new BadRequestObjectResult("Customer doesn't exist");
                    }
                    _dbContext.Customers.Remove(customer);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new OkObjectResult("Customer successfully removed");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new ObjectResult(ex.Message)
                    {
                        StatusCode = 500 // Internal Server Error
                    };
                }
            }
        }

        public async Task<IActionResult> GetCustomer()
        {
            try
            {
                var listCustomer = await _dbContext.Customers.ToListAsync();
                if (listCustomer is null || listCustomer.Count == 0)
                {
                    return new NotFoundObjectResult("Customers not found");
                }
                List<CustomerDto>? listCustomerDto = _mapper.Map<List<CustomerDto>>(listCustomer);
                return new OkObjectResult(listCustomer);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message)
                {
                    StatusCode = 500 // Internal Server Error
                };
            }
        }

        public async Task<IActionResult> GetCustomer(int id)
        {
            try
            {
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == id);
                if (customer is null)
                {
                    return new NotFoundObjectResult("Customer not found");
                }
                else
                {
                    var customerDto = _mapper.Map<UserDto>(customer);
                    return new OkObjectResult(customerDto);
                }

            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message)
                {
                    StatusCode = 500 // Internal Server Error
                };
            }
        }

        public async Task<IActionResult> GetHealthInformationOfCustomer(int id)
        {
            try
            {
                var healthinfo = await _dbContext.HealthInformation.FirstOrDefaultAsync(c => c.CustomerId == id);
                if (healthinfo is null)
                {
                    return new NotFoundObjectResult("Health Information not found");
                }
                else
                {
                    var healthinfoDto = _mapper.Map<HealthInformationDto>(healthinfo);
                    return new OkObjectResult(healthinfoDto);
                }
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message)
                {
                    StatusCode = 500 // Internal Server Error
                };
            }
        }
        public async Task<IActionResult> GetCustomerApproved()
        {
            try
            {
                var listCustomer = await _dbContext.Customers.Where(c => c.IsApproved == (int)ProfileStatus.Approved).ToListAsync();
                var listCustomerDto = _mapper.Map<List<CustomerDto>>(listCustomer);
                return new OkObjectResult(listCustomerDto);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message)
                {
                    StatusCode = 500 // Internal Server Error
                };
            }
        }

        public async Task<IActionResult> GetCustomerPendingApproval()
        {
            try
            {
                var listCustomer = await _dbContext.Customers.Where(c => c.IsApproved == (int)ProfileStatus.Pending).ToListAsync();
                var listCustomerDto = _mapper.Map<List<CustomerDto>>(listCustomer);
                return new OkObjectResult(listCustomerDto);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message)
                {
                    StatusCode = 500 // Internal Server Error
                };
            }
        }

        public async Task<IActionResult> GetCustomerRejected()
        {
            try
            {
                var listCustomer = await _dbContext.Customers.Where(c => c.IsApproved == (int)ProfileStatus.Rejected).ToListAsync();
                var listCustomerDto = _mapper.Map<List<CustomerDto>>(listCustomer);
                return new OkObjectResult(listCustomerDto);
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message)
                {
                    StatusCode = 500 // Internal Server Error
                };
            }
        }
    }
}
