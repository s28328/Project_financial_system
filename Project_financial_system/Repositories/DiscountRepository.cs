using Microsoft.EntityFrameworkCore;
using Project_financial_system.Context;
using Project_financial_system.Models.Domain;
using Project_financial_system.Repositories.Abstraction;

namespace Project_financial_system.Repositories;

public class DiscountRepository:BaseRepository,IDiscountRepository
{
    public DiscountRepository(FinancialContext context) : base(context)
    {
    }

    public async Task<List<Discount>> GetDiscountsForInterval(DateTime startDate, int contractDayInterval,
        CancellationToken cancellationToken)
    {
        DateTime lastDayForPayment = startDate.AddDays(contractDayInterval);
        return  await _context.Discounts.Where(discount =>
            (discount.EndDate >= startDate && discount.StartDate <= lastDayForPayment) ||
            (discount.EndDate == null && discount.StartDate == null)).ToListAsync();
    }
}