namespace CoWorkSpace.Domain.DTOs.Requests;

public class CreateReservationRequestDTO
{
    public int SpaceId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int UserId { get; set; }
}