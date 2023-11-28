﻿using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services
{
    public interface IUserService
    {
        //TODO: Using asynchronous methods
        //TODO: Your code here
        Task<IActionResult> GetUser();
        Task<IActionResult> GetCustomer();
        Task<IActionResult> CustomerRegister(CustomerRegisterRequest customerRegisterRequest);
        Task<IActionResult> CustomerCareDeptRegister(CustomerCareDeptRegisterRequest customerCareDeptRegisterRequest);

        Task<IActionResult> FinancialDeptAdd(FinancialDto financialAddRequest);

        Task<IActionResult> ValidationDeptAdd(ValidationDto validationAddRequest);

        Task<IActionResult> CustomerDel(CustomerDto customerDelRequest);
    }
}
