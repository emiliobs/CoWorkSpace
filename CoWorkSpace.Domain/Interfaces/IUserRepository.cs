using CoWorkSpace.Domain.Entities;

namespace CoWorkSpace.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> GetUserByIdAsync(int id);

    Task<IEnumerable<User>> GetAllAsync();

    Task<User> CreateAsync(User user);

    Task<User> UpdateAsync(User user);

    Task<User> GetUserByEmailAsync(string email);
}