using System.Text.Json;
using System.Text.Json.Serialization;
using AutoMapper;
using ISD_Project.Server.DataAccess;
using ISD_Project.Server.Models;
using ISD_Project.Server.Models.DTOs;
using ISD_Project.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISD_Project.Server;

public class ApprovalStatusService : IApprovalStatusService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidationService _validationService;
    private readonly IMapper _mapper;
    public ApprovalStatusService(ApplicationDbContext dbContext, IValidationService validationService, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _validationService = validationService;
    }

    public async Task<IActionResult> AddApprovalStatusAsync(ApprovalStatusDto approvalStatusDto)
    {
        try
        {
            // Validation
            if (approvalStatusDto is null)
            {
                return new BadRequestObjectResult("Approval status is null");
            }
            if (approvalStatusDto.CustomerId == 0 || approvalStatusDto.InsuranceId == 0 || approvalStatusDto.ValidationDepartmentId == 0)
            {
                return new BadRequestObjectResult("Customer id or insurance id or validation department id is null");
            }

            var customer = await _dbContext.Customers.FindAsync(approvalStatusDto.CustomerId);
            if (customer is null)
            {
                return new NotFoundObjectResult("Customer not found");
            }
            var insurance = await _dbContext.Insurances.FindAsync(approvalStatusDto.InsuranceId);
            if (insurance is null)
            {
                return new NotFoundObjectResult("Insurance not found");
            }
            var validationDepartment = await _dbContext.ValidationDepartments.FindAsync(approvalStatusDto.ValidationDepartmentId);
            if (validationDepartment is null)
            {
                return new NotFoundObjectResult("Validation department not found");
            }

            var approvalStatus = _mapper.Map<ApprovalStatus>(approvalStatusDto);
            approvalStatus.Customer = customer;
            approvalStatus.Insurance = insurance;
            approvalStatus.ValidationDepartment = validationDepartment;
            await _dbContext.ApprovalStatuses.AddAsync(approvalStatus);
            await _dbContext.SaveChangesAsync();
            // Validate customer
            var request = new CustomerValidateRequest { CustomerId = customer.Id, ProfileStatus = approvalStatus.ProfileStatus };
            //await _validationService.ValidateCustomerAsync(request);

            var response = new { approvalStatusId = approvalStatus.Id, message = "Approval status successfully created", value = approvalStatusDto };
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

    public async Task<IActionResult> GetApprovalStatusAsync()
    {
        var approvalStatuses = await _dbContext.ApprovalStatuses.ToListAsync();
        if (approvalStatuses is null)
        {
            return new NotFoundObjectResult("No approval status found");
        }
        var approvalStatusesDto = _mapper.Map<List<ApprovalStatusDto>>(approvalStatuses);
        return new OkObjectResult(approvalStatusesDto);
    }

    public async Task<IActionResult> GetApprovalStatusAsync(ProfileStatus profileStatus)
    {
        var approvalStatuses = await _dbContext.ApprovalStatuses.Where(a => a.ProfileStatus == profileStatus).ToListAsync();
        if (approvalStatuses is null)
        {
            return new NotFoundObjectResult("No approval status found");
        }

        var result = new List<ApprovalStatusReponse>();
        foreach (var item in approvalStatuses)
        {
            var approvalStatusReponse = new ApprovalStatusReponse();
            var customer = await _dbContext.Customers.FindAsync(item.CustomerId);
            if (customer is null)
            {
                return new NotFoundObjectResult("Customer not found");
            }
            var insurance = await _dbContext.Insurances.FindAsync(item.InsuranceId);
            if (insurance is null)
            {
                return new NotFoundObjectResult("Insurance not found");
            }
            var validationDepartment = await _dbContext.ValidationDepartments.FindAsync(item.ValidationDepartmentId);
            if (validationDepartment is null)
            {
                return new NotFoundObjectResult("Validation department not found");
            }
            approvalStatusReponse.CustomerName = item.Customer.Name;
            approvalStatusReponse.CustomerEmail = item.Customer.Email;
            approvalStatusReponse.ValidationDepartmentName = item.ValidationDepartment.Name;
            approvalStatusReponse.InsuranceName = item.Insurance.InsuranceName;
            approvalStatusReponse.ProfileStatus = item.ProfileStatus.ToString();
            approvalStatusReponse.ApprovalDate = item.ApprovalDate;
            approvalStatusReponse.ApprovalComment = item.ApprovalComment;
            result.Add(approvalStatusReponse);
        }
        return new OkObjectResult(result);
    }
}
