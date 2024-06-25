using Project_financial_system.Context;
using Project_financial_system.Models.Domain;
using Project_financial_system.Models.Request;
using Project_financial_system.Repositories.Abstraction;

namespace Project_financial_system.Repositories;

public class PaymentRepository:BaseRepository,IPaymentRepository
{
    public PaymentRepository(FinancialContext context) : base(context)
    {
    }

    public async Task AddPayment(Payment payment, CancellationToken cancellationToken)
    {
        await _context.Payments.AddAsync(payment,cancellationToken);
    }
}