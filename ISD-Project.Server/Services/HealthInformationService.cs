using AutoMapper;
using ISD_Project.Server.DataAccess;
using ISD_Project.Server.Models;
using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISD_Project.Server.Services
{
    public class HealthInformationService : IHealthInformationService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public HealthInformationService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }


        public async Task<IActionResult> AddHealthInformation(HealthInformationDto request)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    //Validate request
                    if (request is null)
                    {
                        return new BadRequestObjectResult("Request is null");
                    }
                    if (request.CustomerId == 0)
                    {
                        return new BadRequestObjectResult("Customer Id is null");
                    }

                    var customer = await _dbContext.Customers.FindAsync(request.CustomerId);
                    if (customer is null)
                    {
                        return new BadRequestObjectResult("Customer not found");
                    }
                    if (customer.HealthInformation != null && customer.HealthInformationId != 0)
                    {
                        return new BadRequestObjectResult("Customer already has health information");
                    }

                    var healthInformation = _mapper.Map<HealthInformation>(request);
                    healthInformation.LastUpdate = DateTime.UtcNow;

                    await _dbContext.HealthInformation.AddAsync(healthInformation);
                    await _dbContext.SaveChangesAsync();

                    //After adding health information, update customer's health information id
                    customer.HealthInformationId = healthInformation.Id;
                    customer.HealthInformation = healthInformation;

                    _dbContext.Customers.Update(customer);
                    await _dbContext.SaveChangesAsync();
                    var response = new { healthInformationId = healthInformation.Id, customerId = customer.Id, message = "Health information successfully created" };
                    await transaction.CommitAsync();
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
    }
}

