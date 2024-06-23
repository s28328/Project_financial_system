using Project_financial_system.Controllers;
using Project_financial_system.Models.Domain;
using Project_financial_system.Models.DTO;

namespace Project_financial_system.Repositories.Abstraction;

public interface ICompanyCustomerRepository: IBaseRepository
{
    void CreateCompanyCustomerAsync(CompanyCustomer companyCustomer, CancellationToken cancellationToken);
    void UpdateCompanyCustomerAsync(CompanyCustomerDto companyCustomer, CompanyCustomer customer,
        int addressId,
        CancellationToken cancellationToken);
    Task<CompanyCustomer?> GetCompanyCustomerByKrs(string krs, CancellationToken cancellationToken);
}