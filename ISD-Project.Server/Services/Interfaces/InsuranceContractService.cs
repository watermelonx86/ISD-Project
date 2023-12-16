using AutoMapper;
using ISD_Project.Server.DataAccess;
using ISD_Project.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISD_Project.Server.Services.Interfaces;

public class InsuranceContractService : IInsuranceContractService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    public InsuranceContractService(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<IActionResult> AddInsuranceContractAsync(InsuranceContractDto insuranceContractDto)
    {
        if (insuranceContractDto is null)
        {
            return new BadRequestObjectResult("Request is null");
        }

        if (insuranceContractDto.ProfileStatus == ProfileStatus.Approved)
        {
            try
            {
                if (insuranceContractDto.CustomerId == 0 || insuranceContractDto.InsuranceId == 0)
                {
                    return new BadRequestObjectResult("CustomerId or InsuranceId is null");
                }
                var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Id == insuranceContractDto.CustomerId);
                if (customer is null)
                {
                    return new BadRequestObjectResult("Customer not found");
                }
                var insurance = await _dbContext.Insurances.FirstOrDefaultAsync(i => i.InsuranceId == insuranceContractDto.InsuranceId);
                if (insurance is null)
                {
                    return new BadRequestObjectResult("Insurance not found");
                }
                var insuranceContract = _mapper.Map<InsuranceContract>(insuranceContractDto);
                insuranceContract.Customer = customer;
                insuranceContract.Insurance = insurance;

                await _dbContext.InsuranceContracts.AddAsync(insuranceContract);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ObjectResult(ex.Message)
                {
                    StatusCode = 500 // Internal Server Error
                };
            }
        }
        else if (insuranceContractDto.ProfileStatus == ProfileStatus.Rejected)
        {

        }


        throw new NotImplementedException();
    }

    public Task<IActionResult> DeleteInsuranceContractAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> GetInsuranceContractAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> GetInsuranceContractsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> UpdateInsuranceContractAsync(InsuranceContract insuranceContract)
    {
        throw new NotImplementedException();
    }
}
