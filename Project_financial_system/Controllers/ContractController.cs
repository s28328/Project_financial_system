using Microsoft.AspNetCore.Mvc;
using Project_financial_system.Models.Domain;
using Project_financial_system.Models.Request;
using Project_financial_system.Services.Abstraction;

namespace Project_financial_system.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContractController:ControllerBase
{
    private readonly IContractService _contractService;

    public ContractController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateContract(RequestContract contract,CancellationToken cancellationToken)
    {
        return StatusCode(201, await _contractService.CreateContract(contract, cancellationToken));
    }

    [HttpPost]
    [Route("/{idContract:int}/payment")]
    public async Task<IActionResult> PayContract(RequestPayment payment, int idContract, CancellationToken cancellationToken)
    {
        return StatusCode(201, await _contractService.PayContract(payment,idContract, cancellationToken));
    }
}