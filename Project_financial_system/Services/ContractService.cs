using CurrencyDotNet;
using CurrencyDotNet.Models;
using Project_financial_system.Exceptions;
using Project_financial_system.Models.Domain;
using Project_financial_system.Models.Request;
using Project_financial_system.Models.Response;
using Project_financial_system.Repositories.Abstraction;
using Project_financial_system.Services.Abstraction;
using Version = Project_financial_system.Models.Domain.Version;

namespace Project_financial_system.Services;

public class ContractService : IContractService
{
    //Discount,Version,Customer
    private readonly IContractRepository _contractRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IDiscountRepository _discountRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IVersionRepository _versionRepository;

    public ContractService(IContractRepository contractRepository, IDiscountRepository discountRepository,
        IVersionRepository versionRepository, ICustomerRepository customerRepository,
        IPaymentRepository paymentRepository)
    {
        _contractRepository = contractRepository;
        _discountRepository = discountRepository;
        _versionRepository = versionRepository;
        _customerRepository = customerRepository;
        _paymentRepository = paymentRepository;
    }

    public async Task<object> CreateContract(RequestContract requestContract, CancellationToken cancellationToken)
    {
        await HasActiveContractForVersion(requestContract.IdCustomer, requestContract.IdVersion, cancellationToken);
        var discount = await GetBestDiscount(requestContract.StartDate, requestContract.DayInterval,
            requestContract.IdCustomer, cancellationToken);
        var version =
            await _versionRepository.GetVersionWithSoftwareByIdAsync(requestContract.IdVersion, cancellationToken);
        ExistsVersion(version);
        await ExistsCustomer(requestContract.IdCustomer);
        var finalPrice = CalculatePrice(version, discount, requestContract.StartDate, requestContract.EndDate,
            requestContract.YearsOfSupport);
        var contract = new Contract
        {
            StartDate = requestContract.StartDate,
            EndDate = requestContract.EndDate,
            Price = finalPrice,
            AmountPaid = 0,
            DayInterval = requestContract.DayInterval,
            IdDiscount = discount.IdDiscount,
            IdVersion = version.IdVersion,
            IsSigned = false,
            IsTerminated = false,
            UpdatesInfo = requestContract.UpdatesInfo,
            YearsOfSupport = requestContract.YearsOfSupport,
            IdCustomer = requestContract.IdCustomer
        };
        var addedContract = await _contractRepository.AddContract(contract, cancellationToken);
        await _contractRepository.SaveChangesAsync(cancellationToken); 
        var createdContract = new CreatedContract
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

    public async Task<object?> PayContract(RequestPayment requestPayment, int idContract,
        CancellationToken cancellationToken)
    {
        var contract = await _contractRepository.GetContractByIdAsync(idContract,cancellationToken);
        ExistsContract(contract);
        IsActiveUnpaidContract(contract);
        var dateOfPayment = DateTime.Today;
        if (IsPaymentInInterval(contract, dateOfPayment))
        {
           return await PayExistsContract(requestPayment, contract, dateOfPayment, idContract, cancellationToken);
        }
        return await RemakeContract(contract, dateOfPayment, requestPayment.Quota, cancellationToken);
    }

    private async Task HasActiveContractForVersion(int idCustomer, int idVersion,CancellationToken cancellationToken)
    {
        var contracts = await _contractRepository.GetActiveContractsForCustomerById(idCustomer,cancellationToken);
        if (contracts.Any(contract => contract.IdVersion == idVersion))
        {
            throw new DomainException("Customer already has contract for this version.");
        }
    }

    private bool IsPaymentInInterval(Contract contract, DateTime dateOfPayment)
    {
        var firstDayOfInterval = contract.StartDate;
        var lastDayOfInterval = firstDayOfInterval.AddDays(contract.DayInterval);
        return dateOfPayment >= firstDayOfInterval && dateOfPayment <= lastDayOfInterval;
    }

    private async Task ExistsCustomer(int contractIdCustomer)
    {
        if (await _customerRepository.GetById(contractIdCustomer) == null)
            throw new DomainException("No customer with provided Id.");
    }

    private void ExistsContract(Contract? contract)
    {
        if (contract == null) throw new DomainException("No contract with provided Id.");
    }

    private void IsActiveUnpaidContract(Contract contract)
    {
        if (contract.IsSigned) throw new DomainException("This contract already full-paid and signed.");

        if (contract.IsTerminated) throw new DomainException("This contract has been terminated.");
    }

    private void ExistsVersion(Version? version)
    {
        if (version == null) throw new DomainException("Provided version doesn`t exist.");
    }

    private decimal CalculatePrice(Version version, Discount? discount, DateTime startDate, DateTime endDate,
        int yearsOfSupport)
    {
        var difference = startDate.Subtract(endDate);
        var daysDifference = difference.Days;
        var contractDurationInYears = (decimal)daysDifference / 365;
        var priceWithoutDiscount = version.Software.PriceForYear * contractDurationInYears;
        decimal priceForSupport = 1000 * (yearsOfSupport - 1);
        priceWithoutDiscount += priceForSupport;
        var finalPrice = priceWithoutDiscount;
        if (discount != null)
            finalPrice *= discount.Percentage;
        return finalPrice;
    }

    private async Task<Discount?> GetBestDiscount(DateTime startDate, int dayInterval, int idCustomer,
        CancellationToken cancellationToken)
    {
        var discounts = await _discountRepository.GetDiscountsForInterval(startDate, dayInterval, cancellationToken);
        if (discounts.Count > 0)
        {
            if (!await _customerRepository.HasContract(idCustomer))
            {
                var index = discounts.FindIndex(discount => discount.Name == "Regular customer discount.");
                if (index != -1)
                    discounts.RemoveAt(index);
            }

            return discounts.MaxBy(disc => disc.Percentage);
        }

        return null;
    }

    private async Task<object> PayExistsContract(RequestPayment requestPayment, Contract contract,
        DateTime dateOfPayment, int idContract,
        CancellationToken cancellationToken)
    {
        var amountToPay = contract.Price - contract.AmountPaid;
        if (requestPayment.Quota > amountToPay) throw new DomainException("Provided quota is higher than needed.");
        var payment = new Payment
        {
            IdContract = idContract,
            IdCustomer = requestPayment.IdCustomer,
            Quota = requestPayment.Quota,
            Date = dateOfPayment
        };
        await _paymentRepository.AddPayment(payment, cancellationToken);
        if (amountToPay == requestPayment.Quota) contract.IsSigned = true;
        await _paymentRepository.SaveChangesAsync(cancellationToken);
        var createdPayment = new CreatedPayment
        {
            IdPayment = payment.IdPayment,
            Date = payment.Date,
            IdContract = payment.IdContract,
            IdCustomer = payment.IdCustomer,
            Quota = payment.Quota
        };
        return new
        {
            message = "Payment is successfully created",
            payment = createdPayment
        };
    }

    private async Task<object> RemakeContract(Contract contract,DateTime dateOfPayment, decimal paidQuota, CancellationToken cancellationToken)
    {
        contract.IsTerminated = true;
        TimeSpan difference = contract.StartDate.Subtract(dateOfPayment);
        var daysDifference = difference.Days;
        var discount = await GetBestDiscount(dateOfPayment, contract.DayInterval, contract.IdCustomer,
            cancellationToken);
        var newContract = new Contract
        {
            StartDate = dateOfPayment,
            EndDate = contract.EndDate.AddDays(daysDifference),
            Price = CalculatePrice(contract.Version, discount, dateOfPayment,
                contract.EndDate.AddDays(daysDifference), contract.YearsOfSupport),
            AmountPaid = paidQuota,
            DayInterval = contract.DayInterval,
            IdDiscount = discount.IdDiscount,
            IdVersion = contract.IdVersion,
            IsSigned = false,
            IsTerminated = false,
            UpdatesInfo = contract.UpdatesInfo,
            YearsOfSupport = contract.YearsOfSupport,
            IdCustomer = contract.IdCustomer
        };
        var addedContract = await _contractRepository.AddContract(contract, cancellationToken);
        await _contractRepository.SaveChangesAsync(cancellationToken);
        var createdContract = new CreatedContract
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
            message = "payment was made late. New contract created.",
            Newcontract = createdContract
        };
    }
}