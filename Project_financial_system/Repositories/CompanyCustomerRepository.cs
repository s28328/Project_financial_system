using Microsoft.EntityFrameworkCore;
using Project_financial_system.Context;
using Project_financial_system.Controllers;
using Project_financial_system.Models.Domain;
using Project_financial_system.Models.DTO;
using Project_financial_system.Repositories.Abstraction;

namespace Project_financial_system.Repositories;

public class CompanyCustomerRepository:BaseRepository, ICompanyCustomerRepository
{
    public CompanyCustomerRepository(FinancialContext context) : base(context)
    {
    }

    public async Task CreateCompanyCustomerAsync(CompanyCustomer companyCustomer, CancellationToken cancellationToken)
    {
        await _context.CompanyCustomers.AddAsync(companyCustomer, cancellationToken);
    }

    public void UpdateCompanyCustomerAsync(CompanyCustomerDto companyCustomer, CompanyCustomer customer,
        int addressId,
        CancellationToken cancellationToken)
    {
        customer.CompanyName = companyCustomer.CompanyName;
        customer.IdAddress = addressId;
        customer.Email = companyCustomer.Email;
        customer.PhoneNumber = companyCustomer.PhoneNumber;
    }

    public async Task<CompanyCustomer?> GetCompanyCustomerByKrs(string krs, CancellationToken cancellationToken)
    {
        return await _context.CompanyCustomers.FirstOrDefaultAsync(customer => customer.KRS== krs,
            cancellationToken);
    }
}