using Microsoft.EntityFrameworkCore;
using Project_financial_system.Context;
using Project_financial_system.Controllers;
using Project_financial_system.Models.Domain;
using Project_financial_system.Models.DTO;
using Project_financial_system.Repositories.Abstraction;

namespace Project_financial_system.Repositories;

public class IndividualCustomerRepository:BaseRepository, IIndividualCustomerRepository
{
    public IndividualCustomerRepository(FinancialContext context) : base(context)
    {
    }

    public void CreateIndividualCustomerAsync(IndividualCustomer individualCustomer, CancellationToken cancellationToken)
    {
        _context.IndividualCustomers.Add(individualCustomer);
    }

    public void RemoveIndividualCustomerAsync(IndividualCustomer customer, int idCustomer,
        CancellationToken cancellationToken)
    {
        customer.IsDeleted = true;
    }

    public void UpdateIndividualCustomerAsync(IndividualCustomerDto individualCustomer, IndividualCustomer customer, int addressId, CancellationToken cancellationToken)
    {
        customer.FirstName = individualCustomer.FirstName;
        customer.LastName = individualCustomer.LastName;
        customer.IdAddress = addressId;
        customer.Email = individualCustomer.Email;
        customer.PhoneNumber = individualCustomer.PhoneNumber;
    }

    public async Task<IndividualCustomer?> GetIndividualCustomerByPeselAsync(string pesel, CancellationToken cancellationToken)
    {
        return await _context.IndividualCustomers.FirstOrDefaultAsync(customer => customer.PESEL == pesel,
            cancellationToken);
    }

    public async Task<IndividualCustomer?> GetIndividualCustomerByIdAsync(int idCustomer, CancellationToken cancellationToken)
    {
        return await _context.IndividualCustomers.FirstOrDefaultAsync(customer => customer.IdCustomer == idCustomer,
            cancellationToken);
    }
}