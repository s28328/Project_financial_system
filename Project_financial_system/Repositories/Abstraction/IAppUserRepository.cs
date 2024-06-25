using Project_financial_system.Models.Domain;

namespace Project_financial_system.Repositories.Abstraction;

public interface IAppUserRepository:IBaseRepository
{
    public Task<int> AddUser(AppUser user, CancellationToken cancellationToken);
    public Task<AppUser> GetUserByLogin(string login, CancellationToken cancellationToken);
    public Task<AppUser> GetUserByRefreshToken(string refreshToken, CancellationToken cancellationToken);
}