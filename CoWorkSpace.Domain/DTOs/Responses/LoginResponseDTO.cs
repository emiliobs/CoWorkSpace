namespace CoWorkSpace.Domain.DTOs.Responses;

public class LoginResponseDTO
{
    public bool IsAuthenticated { get; set; }
    public string Token { get; set; } = null!;
    public String Message { get; set; } = null!;
}