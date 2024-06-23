using Project_financial_system.Controllers;
using Project_financial_system.Exceptions;
using Project_financial_system.Models;
using Project_financial_system.Models.Domain;
using Project_financial_system.Models.DTO;
using Project_financial_system.Models.Response;
using Project_financial_system.Repositories.Abstraction;
using Project_financial_system.Services.Abstraction;

namespace Project_financial_system.Services;

public class CustomerService:ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IIndividualCustomerRepository _individualCustomerRepository;
    private readonly ICompanyCustomerRepository _companyCustomerRepository;
    private readonly IAddressRepository _addressRepository;

    public CustomerService(ICustomerRepository customerRepository, IIndividualCustomerRepository individualCustomerRepository,
        ICompanyCustomerRepository companyCustomerRepository, IAddressRepository addressRepository)
    {
        _customerRepository = customerRepository;
        _individualCustomerRepository = individualCustomerRepository;
        _companyCustomerRepository = companyCustomerRepository;
        _addressRepository = addressRepository;
    }

    public async Task<object?> CreateIndividualCustomerAsync(IndividualCustomerDto individualCustomerDto,CancellationToken cancellationToken)
    {
         await CheckIndividualWithSamePesel(individualCustomerDto.PESEL,cancellationToken);
         var address = await _addressRepository.GetAddressByPostalCode(individualCustomerDto.Address.PostalCode, cancellationToken);
         int addressId = await AddressValidation(individualCustomerDto.Address, cancellationToken);
         IndividualCustomer customer = new IndividualCustomer()
         {
             FirstName = individualCustomerDto.FirstName,
             LastName = individualCustomerDto.LastName,
             Email = individualCustomerDto.Email,
             PhoneNumber = individualCustomerDto.PhoneNumber,
             PESEL = individualCustomerDto.PESEL,
             IdAddress = addressId,
             IsDeleted = false
         };
         await _customerRepository.CreateCustomerAsync(customer, cancellationToken);
         await _individualCustomerRepository.CreateIndividualCustomerAsync(customer, cancellationToken);
         await _customerRepository.SaveChangesAsync(cancellationToken);
         CreatedIndividualCustomerResponse customerResponse = new CreatedIndividualCustomerResponse()
         {
             IdCustomer = customer.IdCustomer,
             FirstName = customer.FirstName,
             LastName = customer.LastName,
             Email = customer.Email,
             PhoneNumber = customer.PhoneNumber,
             PESEL = customer.PESEL,
             IdAddress = addressId
         };
         return new
         {
             message = "Customer was successfully added.",
             customer = customerResponse
         };
    }

    public async Task<object?> CreateCompanyCustomerAsync(CompanyCustomerDto companyCustomerDto,CancellationToken cancellationToken)
    {
        await CheckCompanyWithSameKrs(companyCustomerDto.KRS, cancellationToken);
        int addressId = await AddressValidation(companyCustomerDto.Address, cancellationToken);
        CompanyCustomer customer = new CompanyCustomer()
        {
            IdAddress = addressId,
            PhoneNumber = companyCustomerDto.PhoneNumber,
            Email = companyCustomerDto.Email,
            KRS = companyCustomerDto.KRS,
            CompanyName = companyCustomerDto.CompanyName,
        };
        await _customerRepository.CreateCustomerAsync(customer, cancellationToken);
        await _companyCustomerRepository.CreateCompanyCustomerAsync(customer, cancellationToken);
        await _customerRepository.SaveChangesAsync(cancellationToken);
        CreatedCompanyCustomerResponse customerResponse = new CreatedCompanyCustomerResponse()
        {
            IdCustomer = customer.IdCustomer,
            IdAddress = customer.IdAddress,
            CompanyName = customer.CompanyName,
            PhoneNumber = customer.PhoneNumber,
            Email = customer.Email,
            KRS = customer.KRS
        };
        return new
        {
            message = "Customer was successfully added.",
            customer = customerResponse
        };
    }

    public async Task<object?> RemoveIndividualCustomerAsync(int idCustomer,CancellationToken cancellationToken)
    {
        var customer = await _individualCustomerRepository.GetIndividualCustomerByIdAsync(idCustomer, cancellationToken);
        if (customer == null)
        {
            throw new DomainException("No individual customer with provided id.");
        }

        _individualCustomerRepository.RemoveIndividualCustomerAsync(customer, idCustomer, cancellationToken);
        await _individualCustomerRepository.SaveChangesAsync(cancellationToken);
        return new
        {
            message = "Individual customer was successfully soft deleted.",
            IdCustomer = idCustomer
        };
    }

    public async Task<object?> UpdateIndividualCustomerAsync(IndividualCustomerDto individualCustomer,CancellationToken cancellationToken)
    {
        var customer = await ExistsIndividualWithPesel(individualCustomer.PESEL, cancellationToken);
        var oldCustomer = new IndividualCustomerDto()
        {
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            PESEL = customer.PESEL,
            Address = new AddressDto()
            {
                PostalCode = customer.Address.PostalCode,
                City = customer.Address.City,
                Street = customer.Address.Street
            }
        };
        int addressId = await AddressValidation(individualCustomer.Address, cancellationToken);
        _individualCustomerRepository.UpdateIndividualCustomerAsync(individualCustomer, customer,addressId, cancellationToken);
        await _customerRepository.SaveChangesAsync(cancellationToken);
        return new
        {
            message = "Individual customer was successfully updated.",
            oldValue = oldCustomer,
            newValue = individualCustomer
        };
    }

    public async Task<object?> UpdateCompanyCustomerAsync(CompanyCustomerDto companyCustomer,CancellationToken cancellationToken)
    {
        var customer = await ExistsCompanyWithKRS(companyCustomer.KRS, cancellationToken);
        var oldCustomer = new CompanyCustomerDto()
        {
            CompanyName = companyCustomer.CompanyName,
            KRS = companyCustomer.KRS,
            Email = customer.Email,
            PhoneNumber = customer.PhoneNumber,
            Address = new AddressDto()
            {
                PostalCode = customer.Address.PostalCode,
                City = customer.Address.City,
                Street = customer.Address.Street
            }
        };
        int addressId = await AddressValidation(companyCustomer.Address, cancellationToken);
        _companyCustomerRepository.UpdateCompanyCustomerAsync(companyCustomer, customer,addressId, cancellationToken);
        await _customerRepository.SaveChangesAsync(cancellationToken);
        return new
        {
            message = "Company customer was successfully updated.",
            oldValue = oldCustomer,
            newValue = companyCustomer
        };
    }

    
    
    private async Task CheckIndividualWithSamePesel(string pesel,CancellationToken cancellationToken)
    {
        var customer = await _individualCustomerRepository.GetIndividualCustomerByPeselAsync(pesel,cancellationToken);
        if(customer != null)
        {
            throw new DomainException("Individual Customer with this PESEL is already exists.");
        }
    }

    private async Task<IndividualCustomer> ExistsIndividualWithPesel(string pesel, CancellationToken cancellationToken)
    {
        var customer = await _individualCustomerRepository.GetIndividualCustomerByPeselAsync(pesel,cancellationToken);
        if(customer == null)
        {
            throw new DomainException("No individual customer with provided PESEL.");
        }
        return customer;
    }
    
    private async Task CheckCompanyWithSameKrs(string krs,CancellationToken cancellationToken)
    {
        var customer = await _companyCustomerRepository.GetCompanyCustomerByKrs(krs,cancellationToken);
        if(customer != null)
        {
            throw new DomainException("Individual Customer with this PESEL is already exists.");
        }
    }
    
    private async Task<CompanyCustomer> ExistsCompanyWithKRS(string krs, CancellationToken cancellationToken)
    {
        var customer = await _companyCustomerRepository.GetCompanyCustomerByKrs(krs,cancellationToken);
        if(customer == null)
        {
            throw new DomainException("No company customer with provided KRS.");
        }
        return customer;
    }

    private async Task<int> AddressValidation(AddressDto addressDto,CancellationToken cancellationToken)
    {
        var address = await _addressRepository.GetAddressByPostalCode(addressDto.PostalCode, cancellationToken);
        int addressId = 0;
        if (address == null)
        {
            addressId = await _addressRepository.CreateAddressAsync(addressDto, cancellationToken);
        }
        else
        {
            addressId = address.IdAddress;
        }
        return addressId;
    }
}