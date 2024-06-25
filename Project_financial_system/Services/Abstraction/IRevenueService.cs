using Microsoft.AspNetCore.Mvc;

namespace Project_financial_system.Services.Abstraction;

public interface IRevenueService
{
    public Task<object> GetCurrentRevenue(string currency, CancellationToken cancellationToken);
    public Task<object> GetCurrentRevenueBySoftware(string software, string currency, CancellationToken cancellationToken);
    public Task<object> GetExpectedRevenue(string currency, CancellationToken cancellationToken);
    public Task<object> GetExpectedRevenueBySoftware(string software, string currency, CancellationToken cancellationToken);

}