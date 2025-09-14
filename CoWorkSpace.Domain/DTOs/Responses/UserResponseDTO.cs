using CoWorkSpace.Domain.Enums;

namespace CoWorkSpace.Domain.DTOs.Responses;

public class UserResponseDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsActive { get; set; } = true;
    public string? PhoneNumber { get; set; }
    public Role Role { get; set; }
}