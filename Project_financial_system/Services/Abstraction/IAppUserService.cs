using Microsoft.AspNetCore.Identity.Data;
using Project_financial_system.Models.Request;
using LoginRequest = Project_financial_system.Models.Request.LoginRequest;
using RegisterRequest = Project_financial_system.Models.Request.RegisterRequest;

namespace Project_financial_system.Services.Abstraction;

public interface IAppUserService
{
    public Task<int> RegisterUser(RegisterRequest model, CancellationToken cancellationToken);
    public Task<object?> LoginUser(LoginRequest loginRequest, CancellationToken cancellationToken);
    public Task<object?> RefreshUserToken(RefreshTokenRequest tokenRequest, CancellationToken cancellationToken);
}