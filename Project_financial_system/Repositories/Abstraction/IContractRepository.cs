
using Project_financial_system.Models.Domain;
using Project_financial_system.Models.Request;
using Version = Project_financial_system.Models.Domain.Version;

namespace Project_financial_system.Repositories.Abstraction;

public interface IContractRepository:IBaseRepository
{
    public Task<Contract?> GetContractByIdAsync(int idContract,CancellationToken cancellationToken);
    Task<Contract> AddContract(Contract contract, CancellationToken cancellationToken);
    public Task<List<Contract>> GetActiveContractsForCustomerById(int idCustomer,CancellationToken cancellationToken);
    public Task<List<Contract>> GetSignedContracts(CancellationToken cancellationToken);
    public Task<List<Contract>> GetSignedContractsBySoftware(string software,CancellationToken cancellationToken);
    public Task<List<Contract>> GetActiveContracts(CancellationToken cancellationToken);
    public Task<List<Contract>> GetActiveContractsBySoftware(string software,CancellationToken cancellationToken);


    
    
}