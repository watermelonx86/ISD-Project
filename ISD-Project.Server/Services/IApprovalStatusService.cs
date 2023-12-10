using AutoMapper;
using ISD_Project.Server.Models;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server;

public interface IApprovalStatusService
{
    Task<IActionResult> GetApprovalStatusAsync();
    Task<IActionResult> GetApprovalStatusAsync(ProfileStatus profileStatus);
    Task<IActionResult> AddApprovalStatusAsync(ApprovalStatusDto approvalStatusDto);
}
