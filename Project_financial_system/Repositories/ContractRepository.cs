using Microsoft.EntityFrameworkCore;
using Project_financial_system.Context;
using Project_financial_system.Models.Domain;
using Project_financial_system.Models.Request;
using Project_financial_system.Repositories.Abstraction;
using Version = Project_financial_system.Models.Domain.Version;

namespace Project_financial_system.Repositories;

public class ContractRepository:BaseRepository,IContractRepository
{
    public ContractRepository(FinancialContext context) : base(context)
    {
    }
    

    public Task<Contract?> GetContractByIdAsync(int idContract)
    {
        return _context.Contracts.FirstOrDefaultAsync(contract => contract.IdContract == idContract);
    }

    public async Task<Contract> AddContract(Contract contract, CancellationToken cancellationToken)
    {
        await _context.Contracts.AddAsync(contract, cancellationToken);
        return contract;
        
    }

    public async Task<List<Contract>> GetActiveContractsForCustomerById(int idCustomer)
    {
        var contracts = await _context.Contracts.
            Where(contract => contract.IdCustomer == idCustomer).
            Where(contract => contract.IsSigned == false && contract.IsTerminated ==false).ToListAsync();
        return contracts;
    }
}