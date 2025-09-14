using CoWorkSpace.Domain.DTOs.Requests;
using CoWorkSpace.Domain.DTOs.Responses;

namespace CoWorkSpace.Business.Interfaces;

public interface IAuthService
{
    Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest);

    Task<UserResponseDTO> Register(CreateUserDTO createUser);
}