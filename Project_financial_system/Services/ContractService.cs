using Project_financial_system.Exceptions;
using Project_financial_system.Models.Domain;
using Project_financial_system.Models.Request;
using Project_financial_system.Models.Response;
using Project_financial_system.Repositories.Abstraction;
using Project_financial_system.Services.Abstraction;
using Version = Project_financial_system.Models.Domain.Version;

namespace Project_financial_system.Services;

public class ContractService:IContractService
{
    //Discount,Version,Customer
    private readonly IContractRepository _contractRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IVersionRepository _versionRepository;
    private readonly ICustomerRepository _customerRepository;

    public ContractService(IContractRepository contractRepository, IDiscountRepository discountRepository, IVersionRepository versionRepository, ICustomerRepository customerRepository)
    {
        _contractRepository = contractRepository;
        _discountRepository = discountRepository;
        _versionRepository = versionRepository;
        _customerRepository = customerRepository;
    }

    public async Task<object> CreateContract(RequestContract contract, CancellationToken cancellationToken)
    {
        Discount? discount = await GetBestDiscount(contract.StartDate,contract.DayInterval,contract.IdCustomer,cancellationToken);
        var version = await _versionRepository.GetVersionWithSoftwareByIdAsync(contract.IdVersion, cancellationToken);
        ExistsVersion(version);
        await ExistsCustomer(contract.IdCustomer);
        decimal finalPrice = CalculatePrice(version, discount, contract.StartDate, contract.EndDate, contract.YearsOfSupport);
        var addedContract = await _contractRepository.CreateContract(contract, version, finalPrice, discount, cancellationToken);
        await _contractRepository.SaveChangesAsync(cancellationToken);
        CreatedContract createdContract = new CreatedContract()
        {
            IdContract = addedContract.IdContract,
            StartDate = addedContract.StartDate,
            IdDiscount = addedContract.IdDiscount,
            DayInterval = addedContract.DayInterval,
            EndDate = addedContract.EndDate,
            IdCustomer = addedContract.IdCustomer,
            IdVersion = addedContract.IdVersion,
            YearsOfSupport = addedContract.YearsOfSupport,
            AmountPaid = addedContract.AmountPaid,
            UpdatesInfo = addedContract.UpdatesInfo,
            FinalPrice = addedContract.Price
        };
        return new
        {
            message = "Contract was successfully created.",
            contract = createdContract
        };
    }

    private async Task ExistsCustomer(int contractIdCustomer)
    {
        if (await _customerRepository.GetById(contractIdCustomer) == null)
        {
            throw new DomainException("No customer with provided Id.");
        }
    }

    private void ExistsVersion(Version? version)
    {
        if (version == null)
        {
            throw new DomainException("Provided version doesn`t exist.");
        }
    }

    private decimal CalculatePrice(Version version, Discount? discount, DateTime startDate, DateTime endDate,int yearsOfSupport)
    {
        TimeSpan difference = startDate.Subtract(endDate);
        int daysDifference = difference.Days;
        decimal contractDurationInYears =(decimal)daysDifference / 365;
        decimal priceWithoutDiscount = version.Software.PriceForYear * contractDurationInYears;
        decimal priceForSupport = 1000 * (yearsOfSupport - 1);
        priceWithoutDiscount += priceForSupport;
        decimal finalPrice = priceWithoutDiscount;
        if (discount != null)
            finalPrice *= discount.Percentage;
        return finalPrice;
    }

    private async Task<Discount?> GetBestDiscount(DateTime startDate, int dayInterval,int idCustomer, CancellationToken cancellationToken)
    {
        var discounts = await _discountRepository.GetDiscountsForInterval(startDate, dayInterval,cancellationToken);
        if (discounts.Count > 0)
        {
            if (! await _customerRepository.HasContract(idCustomer))
            {
                int index = discounts.FindIndex(discount => discount.Name == "Regular customer discount.");
                if(index != -1)
                    discounts.RemoveAt(index);
            }
            return discounts.MaxBy(disc => disc.Percentage);
        }
        return null;
    }
}