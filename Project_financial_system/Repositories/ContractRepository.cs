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
    

    public Task<Contract?> GetContractByIdAsync(int idContract,CancellationToken cancellationToken)
    {
        return _context.Contracts.FirstOrDefaultAsync(contract => contract.IdContract == idContract, cancellationToken: cancellationToken);
    }

    public async Task<Contract> AddContract(Contract contract, CancellationToken cancellationToken)
    {
        await _context.Contracts.AddAsync(contract, cancellationToken);
        return contract;
        
    }

    public async Task<List<Contract>> GetActiveContractsForCustomerById(int idCustomer,CancellationToken cancellationToken)
    {
        var contracts = await _context.Contracts.
            Where(contract => contract.IdCustomer == idCustomer).
            Where(contract => contract.IsSigned == false && contract.IsTerminated ==false).ToListAsync( cancellationToken);
        return contracts;
    }

    public async Task<List<Contract>> GetSignedContracts(CancellationToken cancellationToken)
    {
        return await _context.Contracts.Where(contract => contract.IsSigned == true).ToListAsync( cancellationToken);
    }

    public async Task<List<Contract>> GetSignedContractsBySoftware(string software,CancellationToken cancellationToken)
    {
        return await _context.Contracts.
            Where(contract => contract.IsSigned == true).
            Where(contract => contract.Version.Software.Name == software ).ToListAsync(cancellationToken);
    }

    public async Task<List<Contract>> GetActiveContracts(CancellationToken cancellationToken)
    {
        return await _context.Contracts.Where(contract => contract.IsTerminated == false).ToListAsync(cancellationToken);
    }

    public async Task<List<Contract>> GetActiveContractsBySoftware(string software, CancellationToken cancellationToken)
    {
        return await _context.Contracts.
            Where(contract => contract.IsTerminated == false).
            Where(contract => contract.Version.Software.Name == software ).ToListAsync(cancellationToken);
    }
}