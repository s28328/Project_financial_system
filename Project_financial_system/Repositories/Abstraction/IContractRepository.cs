
using Project_financial_system.Models.Domain;
using Project_financial_system.Models.Request;
using Version = Project_financial_system.Models.Domain.Version;

namespace Project_financial_system.Repositories.Abstraction;

public interface IContractRepository:IBaseRepository
{
    public Task<Contract> CreateContract(RequestContract requestContract, Version version, decimal finalPrice, Discount? discount,
        CancellationToken cancellationToken);
}