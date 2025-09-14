using AutoMapper;
using CoWorkSpace.Application.Interfaces;
using CoWorkSpace.Business.Interfaces;
using CoWorkSpace.Domain.DTOs.Requests;
using CoWorkSpace.Domain.DTOs.Responses;
using CoWorkSpace.Domain.Entities;
using CoWorkSpace.Domain.Interfaces;

namespace CoWorkSpace.Business.Services;

public class AuthService : IAuthService
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AuthService(ITokenService tokenService, IUserRepository userRepository, IMapper mapper)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequest)
    {
        var user = await _userRepository.GetUserByEmailAsync(loginRequest.Email);
        var isAuthenticated = user != null && BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash);
        if (isAuthenticated && user!.IsActive)
        {
            var token = _tokenService.GenerateToken(user!);
            return new LoginResponseDTO
            {
                IsAuthenticated = true,
                Token = token,
                Message = "Inicio de sesion exitoso"
            };
        }
        else
        {
            return new LoginResponseDTO
            {
                IsAuthenticated = false,
                Token = string.Empty,
                Message = "Credenciales incorrectas"
            };
        }
    }

    public async Task<UserResponseDTO> Register(CreateUserDTO createUser)
    {
        var user = new User()
        {
            Name = createUser.Name,
            Email = createUser.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUser.Password),
            PhoneNumber = createUser.PhoneNumber,
            Role = createUser.Role,
        };

        var createdUser = await _userRepository.CreateAsync(user);
        return _mapper.Map<UserResponseDTO>(createdUser);
    }
}