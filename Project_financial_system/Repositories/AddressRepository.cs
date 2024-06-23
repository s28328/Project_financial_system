using Microsoft.EntityFrameworkCore;
using Project_financial_system.Context;
using Project_financial_system.Models;
using Project_financial_system.Models.DTO;
using Project_financial_system.Repositories.Abstraction;

namespace Project_financial_system.Repositories;

public class AddressRepository:BaseRepository,IAddressRepository
{
    public AddressRepository(FinancialContext context) : base(context)
    {
    }

    public async Task<int> CreateAddressAsync(AddressDto addressDto, CancellationToken cancellationToken)
    {
        Address address = new Address()
        {
            City = addressDto.City,
            Street = addressDto.Street,
            PostalCode = addressDto.PostalCode
        };
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync(cancellationToken);
        return address.IdAddress;
    }

    public async Task<Address?> GetAddressByPostalCode(string postalCode, CancellationToken cancellationToken)
    {
        return await _context.Addresses.FirstOrDefaultAsync(address => address.PostalCode == postalCode);
    }
}