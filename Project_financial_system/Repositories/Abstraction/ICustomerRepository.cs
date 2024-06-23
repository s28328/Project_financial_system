using Project_financial_system.Models.Domain;

namespace Project_financial_system.Repositories.Abstraction;

public interface ICustomerRepository: IBaseRepository
{
    Task CreateCustomerAsync(Customer customer, CancellationToken cancellationToken);
    public Task<bool> HasContract(int idCustomer);
    public Task<Customer?> GetById(int idCustomer);
}