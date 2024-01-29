using Domain.Models;

namespace WebAPI.Services;

public interface IWAuthService
{
    Task<User> GetUser(string username, string password);
    Task RegisterUser(User user);
    Task<User> ValidateUser(User user);
}