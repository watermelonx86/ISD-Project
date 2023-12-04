using AutoMapper;
using ISD_Project.Server.DataAccess;
using ISD_Project.Server.Models;
using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISD_Project.Server.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly ApplicationDbContext _dbContext;
        public InsuranceService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IActionResult> GetInsurance()
        {
            try
            {
                var listInsurance = await _dbContext.Insurances.ToListAsync();
                return new OkObjectResult(listInsurance);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
