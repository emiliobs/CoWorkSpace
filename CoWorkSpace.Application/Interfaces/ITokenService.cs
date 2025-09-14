using CoWorkSpace.Domain.Entities;

namespace CoWorkSpace.Application.Interfaces;

public interface ITokenService
{
    public string GenerateToken(User user);
}