using Project_financial_system.Models.Request;

namespace Project_financial_system.Services.Abstraction;

public interface IContractService
{
    public Task<object> CreateContract(RequestContract contract,CancellationToken cancellationToken);
}