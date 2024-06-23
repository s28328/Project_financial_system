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

    public async Task<Contract> CreateContract(RequestContract requestContract, Version version, decimal finalPrice,
        Discount? discount, CancellationToken cancellationToken)
    {
        Contract contract = new Contract()
        {
            StartDate = requestContract.StartDate,
            EndDate = requestContract.EndDate,
            Price = finalPrice,
            AmountPaid = 0,
            DayInterval = requestContract.DayInterval,
            IdDiscount = discount.IdDiscount,
            IdVersion = version.IdVersion,
            IsSigned = false,
            UpdatesInfo = requestContract.UpdatesInfo,
            YearsOfSupport = requestContract.YearsOfSupport,
            IdCustomer = requestContract.IdCustomer
        };
        await _context.Contracts.AddAsync(contract, cancellationToken);
        return contract;
    }
}