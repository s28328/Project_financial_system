using Project_financial_system.Controllers;
using Project_financial_system.Models.Domain;
using Project_financial_system.Models.DTO;

namespace Project_financial_system.Repositories.Abstraction;

public interface IIndividualCustomerRepository: IBaseRepository
{
    Task CreateIndividualCustomerAsync(IndividualCustomer individualCustomer,
        CancellationToken cancellationToken);
    void RemoveIndividualCustomerAsync(IndividualCustomer customer, int idCustomer,
        CancellationToken cancellationToken);
    void UpdateIndividualCustomerAsync(IndividualCustomerDto individualCustomer, IndividualCustomer customer,
        int addressId,
        CancellationToken cancellationToken);
    Task<IndividualCustomer?> GetIndividualCustomerByPeselAsync(string PESEL, CancellationToken cancellationToken);
    Task<IndividualCustomer?> GetIndividualCustomerByIdAsync(int idCustomer, CancellationToken cancellationToken);
    
}