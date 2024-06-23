using Project_financial_system.Context;
using Project_financial_system.Models.Domain;
using Project_financial_system.Repositories.Abstraction;

namespace Project_financial_system.Repositories;

public class CustomerRepository:BaseRepository, ICustomerRepository
{
    public CustomerRepository(FinancialContext context) : base(context)
    {
    }

    public void CreateCustomerAsync(Customer customer, CancellationToken cancellationToken)
    {
        _context.Customers.Add(customer);
    }
    
}