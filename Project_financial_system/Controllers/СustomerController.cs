using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project_financial_system.Models.DTO;
using Project_financial_system.Services.Abstraction;

namespace Project_financial_system.Controllers;
[Route("api/[controller]")]
[ApiController]
public class СustomerController:ControllerBase
{
    private ICustomerService _customerService;

    public СustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }
    
    [Authorize(Roles = "user,admin")]
    [Route("/individual")]
    [HttpPost]
    public async Task<IActionResult> CreateIndividualCustomerAsync(IndividualCustomerDto individualCustomerDto,CancellationToken cancellationToken)
    { 
        return Ok(await _customerService.CreateIndividualCustomerAsync(individualCustomerDto, cancellationToken));
    }
    
    [Authorize(Roles = "admin")]
    [Route("/individual/{idCustomer:int}")]
    [HttpDelete]
    public async Task<IActionResult> RemoveIndividualCustomerAsync(int idCustomer,CancellationToken cancellationToken)
    { 
        return Ok(await _customerService.RemoveIndividualCustomerAsync(idCustomer, cancellationToken));
    }
    
    [Authorize(Roles = "admin")]
    [Route("/individual")]
    [HttpPut]
    public async Task<IActionResult> UpdateIndividualCustomerAsync(IndividualCustomerDto individualCustomer,CancellationToken cancellationToken)
    { 
        return Ok(await _customerService.UpdateIndividualCustomerAsync(individualCustomer, cancellationToken));
    }
    
    [Authorize(Roles = "user,admin")]
    [Route("/company")]
    [HttpPost]
    public async Task<IActionResult> CreateCompanyCustomerAsync(CompanyCustomerDto companyCustomerDto,CancellationToken cancellationToken)
    { 
        return Ok(await _customerService.CreateCompanyCustomerAsync(companyCustomerDto, cancellationToken));
    }
    
    [Authorize(Roles = "admin")]
    [Route("/company")]
    [HttpPut]
    public async Task<IActionResult> UpdateCompanyCustomerAsync(CompanyCustomerDto companyCustomer,CancellationToken cancellationToken)
    { 
        return Ok(await _customerService.UpdateCompanyCustomerAsync(companyCustomer, cancellationToken));
    }
}