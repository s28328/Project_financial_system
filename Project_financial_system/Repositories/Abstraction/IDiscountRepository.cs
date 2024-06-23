using Project_financial_system.Models.Domain;

namespace Project_financial_system.Repositories.Abstraction;

public interface IDiscountRepository:IBaseRepository
{
    Task<List<Discount>> GetDiscountsForInterval(DateTime today, int contractDayInterval,
        CancellationToken cancellationToken);
}