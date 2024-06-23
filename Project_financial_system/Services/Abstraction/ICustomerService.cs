using Project_financial_system.Controllers;
using Project_financial_system.Models.Request;

namespace Project_financial_system.Services.Abstraction;

public interface ICustomerService
{
    Task<object?> CreateIndividualCustomerAsync(CreateIndividualCustomer individualCustomer);
    Task<object?> CreateCompanyCustomerAsync(CreateCompanyCustomer companyCustomer);
    Task<object?> RemoveIndividualCustomerAsync(int idCustomer);
    Task<object?> UpdateIndividualCustomerAsync(UpdateIndividualCustomer individualCustomer);
    Task<object?> UpdateCompanyCustomerAsync(UpdateCompanyCustomer companyCustomer);
}