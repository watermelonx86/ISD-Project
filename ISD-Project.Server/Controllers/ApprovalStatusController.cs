using ISD_Project.Server.Models;
using ISD_Project.Server.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ISD_Project.Server.Controllers;

[Route("api/[controller]")]

[ApiController]
public class ApprovalStatusController : ControllerBase
{
    private readonly IApprovalStatusService _approvalStatusService;
    public ApprovalStatusController(IApprovalStatusService approvalStatusService)
    {
        _approvalStatusService = approvalStatusService;
    }
    [HttpGet("get-approval-status")]
    public Task<IActionResult> GetApprovalStatus()
    {
        return _approvalStatusService.GetApprovalStatusAsync();
    }

    [HttpGet("get-approval-status/{profileStatus}")]
    public Task<IActionResult> GetApprovalStatus(ProfileStatus profileStatus)
    {
        return _approvalStatusService.GetApprovalStatusAsync(profileStatus);
    }

    [HttpPost("add-approval-status")]
    public Task<IActionResult> AddApprovalStatus(ApprovalStatusDto approvalStatusDto)
    {
        return _approvalStatusService.AddApprovalStatusAsync(approvalStatusDto);
    }
}
