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
                var listCustomerDto = _mapper.Map<List<CustomerDto>>(listCustomer);
                return new OkObjectResult(listCustomerDto);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
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
            catch (Exception)
            {
                return new StatusCodeResult(500);
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
            catch (Exception)
            {
                return new StatusCodeResult(500);
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
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
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
        public async Task<IActionResult> FinancialDeptAdd(UserDto request)
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
                    var financialDept = _mapper.Map<FinancialDepartment>(request);
                    _dbContext.FinancialDepartments.Add(financialDept);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new OkObjectResult("Financial Department successfully created");
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return new StatusCodeResult(500); // Internal Server Error
                }
            }
        }
        public async Task<IActionResult> ValidationDeptAdd(UserDto request)
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
                    var validationDept = _mapper.Map<ValidationDepartment>(request);
                    _dbContext.ValidationDepartments.Add(validationDept);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new OkObjectResult("Validation Department successfully created");
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return new StatusCodeResult(500); // Internal Server Error
                }
            }
        }
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
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return new StatusCodeResult(500); // Internal Server Error
                }
            }
        }
        public async Task<IActionResult> DeleteCustomerCare(int id)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var customerCareDepartment = await _dbContext.CustomerCareDepartments.FindAsync(id);
                    if (customerCareDepartment is null)
                    {
                        return new BadRequestObjectResult("Customer Care Department doesn't exist");
                    }
                    _dbContext.CustomerCareDepartments.Remove(customerCareDepartment);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new OkObjectResult("Customer Care Department successfully removed");
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return new StatusCodeResult(500); // Internal Server Error
                }
            }
        }
        public async Task<IActionResult> DeleteFinancialDept(int id)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var financialDepartment = await _dbContext.FinancialDepartments.FindAsync(id);
                    if (financialDepartment is null)
                    {
                        return new BadRequestObjectResult("Financial Department doesn't exist");
                    }
                    _dbContext.FinancialDepartments.Remove(financialDepartment);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new OkObjectResult("Financial Department successfully removed");
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return new StatusCodeResult(500); // Internal Server Error
                }
            }
        }
        public async Task<IActionResult> DeleteValidationDept(int id)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var validationDepartment = await _dbContext.ValidationDepartments.FindAsync(id);

                    if (validationDepartment is null)
                    {
                        return new BadRequestObjectResult("Validation Department doesn't exist");
                    }
                    _dbContext.ValidationDepartments.Remove(validationDepartment);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new OkObjectResult("Validation Department successfully removed");
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
