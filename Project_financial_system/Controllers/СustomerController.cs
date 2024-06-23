using Microsoft.AspNetCore.Mvc;
using Project_financial_system.Models.Request;
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
    [Route("/individual")]
    [HttpPost]
    public async Task<IActionResult> CreateIndividualCustomerAsync(CreateIndividualCustomer individualCustomer)
    { 
        return Ok(await _customerService.CreateIndividualCustomerAsync(individualCustomer));
    }
    
    [Route("/individual/{idCustomer:int}")]
    [HttpDelete]
    public async Task<IActionResult> RemoveIndividualCustomerAsync(int idCustomer)
    { 
        return Ok(await _customerService.RemoveIndividualCustomerAsync(idCustomer));
    }
    [Route("/individual")]
    [HttpPut]
    public async Task<IActionResult> UpdateIndividualCustomerAsync(UpdateIndividualCustomer individualCustomer)
    { 
        return Ok(await _customerService.UpdateIndividualCustomerAsync(individualCustomer));
    }
    
    
    [Route("/company")]
    [HttpPost]
    public async Task<IActionResult> CreateCompanyCustomerAsync(CreateCompanyCustomer companyCustomer)
    { 
        return Ok(await _customerService.CreateCompanyCustomerAsync(companyCustomer));
    }
    [Route("/company")]
    [HttpPut]
    public async Task<IActionResult> UpdateCompanyCustomerAsync(UpdateCompanyCustomer companyCustomer)
    { 
        return Ok(await _customerService.UpdateCompanyCustomerAsync(companyCustomer));
    }
}