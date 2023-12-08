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
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message)
                {
                    StatusCode = 500 // Internal Server Error
                };
            }
        }

        public Task<IActionResult> GetUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> GetUserByRole(RoleType role)
        {
            throw new NotImplementedException();
        }
        // public async Task<IActionResult> AddCustomerCareDept(UserDto request)
        // {
        //     using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        //     {
        //         try
        //         {
        //             if (request is null)
        //             {
        //                 return new BadRequestObjectResult("Request is null");
        //             }
        //             if (await _dbContext.Users.AnyAsync(u => u.Email == request.Email))
        //             {
        //                 return new BadRequestObjectResult("Email already exists");
        //             }
        //             var customerCareDept = _mapper.Map<CustomerCareDepartment>(request);
        //             _dbContext.CustomerCareDepartments.Add(customerCareDept);
        //             await _dbContext.SaveChangesAsync();
        //             await transaction.CommitAsync();
        //             var response = new { message = "Customer Care Department successfully created", userId = customerCareDept.Id };
        //             return new OkObjectResult(response);
        //         }
        //         catch (Exception ex)
        //         {
        //             await transaction.RollbackAsync();
        //             return new ObjectResult(ex.Message)
        //             {
        //                 StatusCode = 500 // Internal Server Error
        //             };
        //         }

        //     }
        // }
        // public async Task<IActionResult> FinancialDeptAdd(UserDto request)
        // {
        //     using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        //     {
        //         try
        //         {
        //             if (request is null)
        //             {
        //                 return new BadRequestObjectResult("Request is null");
        //             }
        //             if (await _dbContext.Users.AnyAsync(u => u.Email == request.Email))
        //             {
        //                 return new BadRequestObjectResult("Email already exists");
        //             }
        //             var financialDept = _mapper.Map<FinancialDepartment>(request);
        //             _dbContext.FinancialDepartments.Add(financialDept);
        //             await _dbContext.SaveChangesAsync();
        //             await transaction.CommitAsync();
        //             var response = new { message = "Financial Department successfully created", userId = financialDept.Id };

        //             return new OkObjectResult(response);
        //         }
        //         catch (Exception ex)
        //         {
        //             await transaction.RollbackAsync();
        //             return new ObjectResult(ex.Message)
        //             {
        //                 StatusCode = 500 // Internal Server Error
        //             };
        //         }
        //     }
        // }
        // public async Task<IActionResult> ValidationDeptAdd(UserDto request)
        // {
        //     using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        //     {
        //         try
        //         {
        //             if (request is null)
        //             {
        //                 return new BadRequestObjectResult("Request is null");
        //             }
        //             if (await _dbContext.Users.AnyAsync(u => u.Email == request.Email))
        //             {
        //                 return new BadRequestObjectResult("Email already exists");
        //             }
        //             var validationDept = _mapper.Map<ValidationDepartment>(request);
        //             _dbContext.ValidationDepartments.Add(validationDept);
        //             await _dbContext.SaveChangesAsync();
        //             await transaction.CommitAsync();
        //             var response = new { message = "Financial Department successfully created", userId = validationDept.Id };
        //             return new OkObjectResult("Validation Department successfully created");
        //         }
        //         catch (Exception ex)
        //         {
        //             await transaction.RollbackAsync();
        //             return new ObjectResult(ex.Message)
        //             {
        //                 StatusCode = 500 // Internal Server Error
        //             };
        //         }
        //     }
        // }

        // public async Task<IActionResult> DeleteCustomerCare(int id)
        // {
        //     using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        //     {
        //         try
        //         {
        //             var customerCareDepartment = await _dbContext.CustomerCareDepartments.FindAsync(id);
        //             if (customerCareDepartment is null)
        //             {
        //                 return new BadRequestObjectResult("Customer Care Department doesn't exist");
        //             }
        //             _dbContext.CustomerCareDepartments.Remove(customerCareDepartment);
        //             await _dbContext.SaveChangesAsync();
        //             await transaction.CommitAsync();
        //             return new OkObjectResult("Customer Care Department successfully removed");
        //         }
        //         catch (Exception ex)
        //         {
        //             await transaction.RollbackAsync();
        //             return new ObjectResult(ex.Message)
        //             {
        //                 StatusCode = 500 // Internal Server Error
        //             };
        //         }
        //     }
        // }
        // public async Task<IActionResult> DeleteFinancialDept(int id)
        // {
        //     using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        //     {
        //         try
        //         {
        //             var financialDepartment = await _dbContext.FinancialDepartments.FindAsync(id);
        //             if (financialDepartment is null)
        //             {
        //                 return new BadRequestObjectResult("Financial Department doesn't exist");
        //             }
        //             _dbContext.FinancialDepartments.Remove(financialDepartment);
        //             await _dbContext.SaveChangesAsync();
        //             await transaction.CommitAsync();
        //             return new OkObjectResult("Financial Department successfully removed");
        //         }
        //         catch (Exception ex)
        //         {
        //             await transaction.RollbackAsync();
        //             return new ObjectResult(ex.Message)
        //             {
        //                 StatusCode = 500 // Internal Server Error
        //             };
        //         }
        //     }
        // }
        // public async Task<IActionResult> DeleteValidationDept(int id)
        // {
        //     using (var transaction = await _dbContext.Database.BeginTransactionAsync())
        //     {
        //         try
        //         {
        //             var validationDepartment = await _dbContext.ValidationDepartments.FindAsync(id);

        //             if (validationDepartment is null)
        //             {
        //                 return new BadRequestObjectResult("Validation Department doesn't exist");
        //             }
        //             _dbContext.ValidationDepartments.Remove(validationDepartment);
        //             await _dbContext.SaveChangesAsync();
        //             await transaction.CommitAsync();
        //             return new OkObjectResult("Validation Department successfully removed");
        //         }
        //         catch (Exception ex)
        //         {
        //             await transaction.RollbackAsync();
        //             return new ObjectResult(ex.Message)
        //             {
        //                 StatusCode = 500 // Internal Server Error
        //             };
        //         }
        //     }
        // }


    }
}
