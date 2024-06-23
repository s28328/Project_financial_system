using Project_financial_system.Controllers;
using Project_financial_system.Models.DTO;

namespace Project_financial_system.Services.Abstraction;

public interface ICustomerService
{
    Task<object?> CreateIndividualCustomerAsync(IndividualCustomerDto individualCustomerDto,
        CancellationToken cancellationToken);
    Task<object?> CreateCompanyCustomerAsync(CompanyCustomerDto companyCustomerDto,CancellationToken cancellationToken);
    Task<object?> RemoveIndividualCustomerAsync(int idCustomer,CancellationToken cancellationToken);
    Task<object?> UpdateIndividualCustomerAsync(IndividualCustomerDto individualCustomer,
        CancellationToken cancellationToken);

    public Task<object?> UpdateCompanyCustomerAsync(CompanyCustomerDto companyCustomer,
        CancellationToken cancellationToken);
}