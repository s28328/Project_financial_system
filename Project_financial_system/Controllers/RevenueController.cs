using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_financial_system.Services.Abstraction;

namespace Project_financial_system.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RevenueController:ControllerBase
{
    private readonly IRevenueService _revenueService;

    public RevenueController(IRevenueService revenueService)
    {
        _revenueService = revenueService;
    }
    
    [Authorize(Roles = "user,admin")]
    [Route("/current")]
    [HttpGet]
    public async Task<IActionResult> GeCurrentRevenue(string currency,CancellationToken cancellationToken)
    {
        return StatusCode(200, await _revenueService.GetCurrentRevenue(currency,cancellationToken));
    }
    
    [Authorize(Roles = "user,admin")]
    [Route("/expected")]
    [HttpGet]
    public async Task<IActionResult> GeExcpectedRevenue(string currency,CancellationToken cancellationToken)
    {
        return StatusCode(200, await _revenueService.GetExpectedRevenue(currency,cancellationToken));
    }
    
    [Authorize(Roles = "user,admin")]
    [Route("/current/{software}")]
    [HttpGet]
    public async Task<IActionResult> GeCurrentRevenueForSoftware(string currency,string software,CancellationToken cancellationToken)
    {
        return StatusCode(200, await _revenueService.GetCurrentRevenue(currency,cancellationToken));
    }
    
    [Authorize(Roles = "user,admin")]
    [Route("/expected/{software}")]
    [HttpGet]
    public async Task<IActionResult> GeExpectedRevenueForSoftware(string currency,string software,CancellationToken cancellationToken)
    {
        return StatusCode(200, await _revenueService.GetCurrentRevenue(currency,cancellationToken));
    }
    
}