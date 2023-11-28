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
                var listCustomerDto = new List<CustomerDto>();
                foreach (var customer in listCustomer)
                {
                    var customerDto = new CustomerDto
                    {
                        Name = customer.Name,
                        Gender = customer.Gender,
                        Address = customer.Address,
                        Email = customer.Email,
                        IdentityDocumentId = customer.IdentityDocumentId,
                        PhoneNumber = customer.PhoneNumber
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
                foreach (var user in listUser)
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

        public async Task<IActionResult> CustomerRegister(CustomerRegisterRequest request)
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

        public async Task<IActionResult> CustomerCareDeptRegister(CustomerCareDeptRegisterRequest request)
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

        public async Task<IActionResult> FinancialDeptAdd(FinancialDto request)
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

                    var financial = new FinancialDepartment
                    {
                        Name = request.Name,
                        Gender = request.Gender,
                        Address = request.Address,
                        Email = request.Email,
                        IdentityDocumentId = request.IdentityDocumentId,
                        PhoneNumber = request.PhoneNumber
                    };
                    _dbContext.FinancialDepartments.Add(financial);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new OkObjectResult("Financial successfully created");

                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return new StatusCodeResult(500); // Internal Server Error
                }
            }
        }

        public async Task<IActionResult> ValidationDeptAdd(ValidationDto request)
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

                    var validation = new ValidationDepartment
                    {
                        Name = request.Name,
                        Gender = request.Gender,
                        Address = request.Address,
                        Email = request.Email,
                        IdentityDocumentId = request.IdentityDocumentId,
                        PhoneNumber = request.PhoneNumber
                    };
                    _dbContext.ValidationDepartments.Add(validation);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return new OkObjectResult("Validation successfully created");

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
                    Customer customer = _dbContext.Customers.Find(id);

                    if (customer is null)
                    {
                        return new BadRequestObjectResult("Request is null");
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
                    CustomerCareDepartment customerCareDepartment = _dbContext.CustomerCareDepartments.Find(id);

                    if (customerCareDepartment is null)
                    {
                        return new BadRequestObjectResult("Request is null");
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
                    FinancialDepartment financialDepartment = _dbContext.FinancialDepartments.Find(id);

                    if (financialDepartment is null)
                    {
                        return new BadRequestObjectResult("Request is null");
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

        public async Task<IActionResult> DeleteValdationDept(int id)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    ValidationDepartment validationDepartment = _dbContext.ValidationDepartments.Find(id);

                    if (validationDepartment is null)
                    {
                        return new BadRequestObjectResult("Request is null");
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
