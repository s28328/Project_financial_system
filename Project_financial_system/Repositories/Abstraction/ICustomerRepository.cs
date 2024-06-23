using Project_financial_system.Models.Domain;

namespace Project_financial_system.Repositories.Abstraction;

public interface ICustomerRepository: IBaseRepository
{
    void CreateCustomerAsync(Customer customer, CancellationToken cancellationToken);
}