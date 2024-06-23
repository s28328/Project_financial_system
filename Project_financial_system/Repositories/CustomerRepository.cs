using Microsoft.EntityFrameworkCore;
using Project_financial_system.Context;
using Project_financial_system.Models.Domain;
using Project_financial_system.Repositories.Abstraction;

namespace Project_financial_system.Repositories;

public class CustomerRepository:BaseRepository, ICustomerRepository
{
    public CustomerRepository(FinancialContext context) : base(context)
    {
    }

    public async Task CreateCustomerAsync(Customer customer, CancellationToken cancellationToken)
    {
        await _context.Customers.AddAsync(customer, cancellationToken);
    }

    public async Task<bool> HasContract(int idCustomer)
    {
        return await _context.Contracts.Where(contract => contract.IdCustomer == idCustomer).CountAsync() >= 1;
    }

    public async Task<Customer?> GetById(int idCustomer)
    {
        return await _context.Customers.FirstOrDefaultAsync(customer => customer.IdCustomer == idCustomer);
    }
}