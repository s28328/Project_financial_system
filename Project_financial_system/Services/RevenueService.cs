using CurrencyDotNet;
using CurrencyDotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_financial_system.Repositories.Abstraction;
using Project_financial_system.Services.Abstraction;

namespace Project_financial_system.Services;

public class RevenueService:IRevenueService
{
    private readonly IContractRepository _contractRepository;
    private IConfiguration _configuration;

    public RevenueService(IContractRepository contractRepository, IConfiguration configuration)
    {
        _contractRepository = contractRepository;
        _configuration = configuration;
    }

    public async Task<object> GetCurrentRevenue(string currency, CancellationToken cancellationToken)
    {
        var contracts = await _contractRepository.GetSignedContracts(cancellationToken);
        var revenue = contracts.Sum(contract => contract.AmountPaid);
        decimal conversionRate = await GetConversionRate(currency);
        revenue *= conversionRate;
        return new
        {
            message = "Current company revenue",
            currency,
            revenue
        };
    }

    public async Task<object> GetCurrentRevenueBySoftware(string software, string currency,CancellationToken cancellationToken)
    {
        var contracts = await _contractRepository.GetSignedContractsBySoftware(software,cancellationToken);
        var revenue = contracts.Sum(contract => contract.AmountPaid);
        decimal conversionRate = await GetConversionRate(currency);
        revenue *= conversionRate;
        return new
        {
            message = "Current company revenue",
            currency,
            revenue
        };
    }

    public async Task<object> GetExpectedRevenue(string currency, CancellationToken cancellationToken)
    {
        
        var contracts = await _contractRepository.GetActiveContracts(cancellationToken);
        var revenue = contracts.Sum(contract => contract.AmountPaid);
        decimal conversionRate = await GetConversionRate(currency);
        revenue *= conversionRate;
        return new
        {
            message = "Excpected company revenue",
            currency,
            revenue
        };
    }

    public async Task<object> GetExpectedRevenueBySoftware(string software, string currency, CancellationToken cancellationToken)
    {
        var contracts = await _contractRepository.GetActiveContractsBySoftware(software,cancellationToken);
        var revenue = contracts.Sum(contract => contract.AmountPaid);
        decimal conversionRate = await GetConversionRate(currency);
        revenue *= conversionRate;
        return new
        {
            message = "Current company revenue",
            currency,
            revenue
        };
        
    }

    private async Task<JObject> FetchDataFromApi(string url)
        {
        HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return JObject.Parse(responseBody);
        }

        private async Task<decimal> GetConversionRate(string currency)
        {
            string APIUrl = $"https://v6.exchangerate-api.com/v6/{_configuration["API_Keys:Currency"]}/pair/PLN/{currency}";
            var response = await FetchDataFromApi(APIUrl);
            var conversionRateObject = response["conversion_rate"];
            return  decimal.Parse(conversionRateObject.ToString());
        }
}