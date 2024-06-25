using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project_financial_system.Helpers;
using Project_financial_system.Models.Domain;
using Project_financial_system.Models.Request;
using Project_financial_system.Repositories.Abstraction;
using Project_financial_system.Services.Abstraction;

namespace Project_financial_system.Services;

public class AppUserService:IAppUserService
{
    private readonly IAppUserRepository _userRepository;
    private readonly IConfiguration _configuration;

    public AppUserService(IAppUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    public async Task<int> RegisterUser(RegisterRequest model, CancellationToken cancellationToken)
    {
        var hashedPasswordAndSalt = SecurityHelpers.GetHashedPasswordAndSalt(model.Password);

        var user = new AppUser()
        {
            Email = model.Email,
            Login = model.Login,
            Role = model.Role,
            Password = hashedPasswordAndSalt.Item1,
            Salt = hashedPasswordAndSalt.Item2,
            RefreshToken = SecurityHelpers.GenerateRefreshToken(),
            RefreshTokenExp = DateTime.Now.AddDays(1)
        };
        await _userRepository.AddUser(user, cancellationToken);
        return 1;
    }

    public async Task<Object?> LoginUser(LoginRequest loginRequest, CancellationToken cancellationToken)
    {
        AppUser user = await _userRepository.GetUserByLogin(loginRequest.Login, cancellationToken);

        string passwordHashFromDb = user.Password;
        string curHashedPassword = SecurityHelpers.GetHashedPasswordWithSalt(loginRequest.Password, user.Salt);

        if (passwordHashFromDb != curHashedPassword)
        {
            return new UnauthorizedResult();
        }
        var userClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Login)
        };
        
        if (user.Role == "admin")
        {
            userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
        }
        else
        {
            userClaims.Add(new Claim(ClaimTypes.Role, "user"));
        }

        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken token = new JwtSecurityToken(
            issuer: "https://localhost:5038",
            audience: "https://localhost:5038",
            claims: userClaims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _userRepository.SaveChangesAsync(cancellationToken);
        return new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(token),
            refreshToken = user.RefreshToken
        };
    }

    public async Task<Object?> RefreshUserToken(RefreshTokenRequest tokenRequest, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByRefreshToken(tokenRequest.RefreshToken, cancellationToken);
        if (user == null)
        {
            throw new SecurityTokenException("Invalid refresh token");
        }

        if (user.RefreshTokenExp < DateTime.Now)
        {
            throw new SecurityTokenException("Refresh token expired");
        }
        
        var userClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Login)
        };

        // Assuming you have a property `Role` in your user model
        if (user.Role == "admin")
        {
            userClaims.Add(new Claim(ClaimTypes.Role, "admin"));
        }
        else
        {
            userClaims.Add(new Claim(ClaimTypes.Role, "user"));
        }


        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));

        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        JwtSecurityToken jwtToken = new JwtSecurityToken(
            issuer: "https://localhost:5038",
            audience: "https://localhost:5038",
            claims: userClaims,
            expires: DateTime.Now.AddMinutes(10),
            signingCredentials: creds
        );

        user.RefreshToken = SecurityHelpers.GenerateRefreshToken();
        user.RefreshTokenExp = DateTime.Now.AddDays(1);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return new
        {
            accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken),
            refreshToken = user.RefreshToken
        };
    }
}