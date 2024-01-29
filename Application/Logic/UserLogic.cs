using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{
    private readonly IUserDao userDao;

    public UserLogic(IUserDao userDao)
    {
        this.userDao = userDao;
    }

    public async Task<User> CreateAsync(UserCreationDto dto)
    {
        User? existing = await userDao.GetByUsernameAsync(dto.UserName);
        if (existing != null)
        {
            throw new Exception("Username is already taken! ");
        }

        ValidateData(dto);
        User toCreate = new User
        {
            UserName = dto.UserName,
            Password = dto.Password
        };

        User created = await userDao.CreateAsync(toCreate);
        
        return created;
    }
    private static void ValidateData(UserCreationDto userToCreate)
    {
        string userName = userToCreate.UserName;
        string password = userToCreate.Password;

        if (userName.Length <= 1)
        {
            throw new Exception("Username must contain at least 1 character! ");
        }
        else if (userName.Length >= 15) 
        {
            throw new Exception("Username must be less then 15 characters! ");
        }

        if (password.Length <= 3)
        {
            throw new Exception("Password must contain at least 3 character! ");
        } 
        else if (password.Length >= 15)
        {
            throw new Exception("Password must be less then 15 characters! ");
        }
    }
    
    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDto searchParameters)
    {
        return userDao.GetAsync(searchParameters);
    }
}