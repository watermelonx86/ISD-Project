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
            InsuranceContract? insuranceContract = _mapper.Map<InsuranceContract>(insuranceContractDto);
            insuranceContract.CustomerId = insuranceContractDto.CustomerId;
            insuranceContract.Customer = customer;
            insuranceContract.InsuranceId = insuranceContractDto.InsuranceId;
            insuranceContract.Insurance = insurance;
            await _dbContext.InsuranceContracts.AddAsync(insuranceContract);
            await _dbContext.SaveChangesAsync();
            var response = new { customerId = insuranceContractDto.CustomerId, insuranceId = insuranceContractDto.InsuranceId, message = "Insurance Contract successfully created" };
            return new OkObjectResult(response);
        }
        catch (Exception ex)
        {
            return new ObjectResult(ex.Message)
            {
                StatusCode = 500 // Internal Server Error
            };
        }
    }

    public Task<IActionResult> DeleteInsuranceContractAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IActionResult> GetInsuranceContractAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IActionResult> GetInsuranceContractsAsync()
    {
        var InsuranceContracts = await _dbContext.InsuranceContracts.ToListAsync();
        if (InsuranceContracts is null)
        {
            return new NotFoundObjectResult("No insurance contracts found");
        }
        List<InsuranceContractDto> listInsuranceContractDto = _mapper.Map<List<InsuranceContractDto>>(InsuranceContracts);
        return new OkObjectResult(listInsuranceContractDto);
    }

    public Task<IActionResult> UpdateInsuranceContractAsync(InsuranceContract insuranceContract)
    {
        throw new NotImplementedException();
    }
}
