using Project_financial_system.Models;
using Project_financial_system.Models.DTO;

namespace Project_financial_system.Repositories.Abstraction;

public interface IAddressRepository: IBaseRepository
{
    Task<int> CreateAddressAsync(AddressDto addressDto, CancellationToken cancellationToken);
    Task<Address?> GetAddressByPostalCode(string postalCode, CancellationToken cancellationToken);

}