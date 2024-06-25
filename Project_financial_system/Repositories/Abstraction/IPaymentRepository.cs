using Project_financial_system.Models.Domain;
using Project_financial_system.Models.Request;

namespace Project_financial_system.Repositories.Abstraction;

public interface IPaymentRepository:IBaseRepository
{
    Task AddPayment(Payment payment, CancellationToken cancellationToken);
}