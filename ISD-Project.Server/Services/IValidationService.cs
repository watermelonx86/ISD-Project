﻿using ISD_Project.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Services
{
    public interface IValidationService
    {
        Task<IActionResult> ValidateCustomer(CustomerValidateRequest request);
        Task<IActionResult> ValidateUserAccount(UserAccountValidateRequest request);
    }
}
