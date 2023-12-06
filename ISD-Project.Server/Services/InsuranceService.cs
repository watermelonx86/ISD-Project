using AutoMapper;
using ISD_Project.Server.DataAccess;
using ISD_Project.Server.Models;
using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISD_Project.Server.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public InsuranceService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<IActionResult> GetInsurance()
        {
            try
            {
                var listInsurance = await _dbContext.Insurances.ToListAsync();
                var listInsuranceDto = _mapper.Map<List<Insurance>, List<InsuranceDto>>(listInsurance);
                return new OkObjectResult(listInsuranceDto);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
