using CoWorkSpace.Domain.Enums;

namespace CoWorkSpace.Domain.DTOs.Requests;

public class CreateUserDTO
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public Role Role { get; set; }
}